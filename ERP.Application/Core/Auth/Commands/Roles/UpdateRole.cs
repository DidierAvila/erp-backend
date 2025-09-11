using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Roles
{
    public class UpdateRole
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Role> _roleRepository;
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;

        public UpdateRole(IRepositoryBase<ERP.Domain.Entities.Auth.Role> roleRepository, IRepositoryBase<Permission> permissionRepository, IRolePermissionRepository rolePermissionRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> HandleAsync(Guid id, UpdateRoleDto updateRoleDto, CancellationToken cancellationToken)
        {
            // Find existing role
            var role = await _roleRepository.Find(x => x.Id == id, cancellationToken);
            if (role == null)
                throw new KeyNotFoundException("Role not found");

            // Check if name already exists (excluding current role)
            if (!string.IsNullOrWhiteSpace(updateRoleDto.Name))
            {
                var existingRole = await _roleRepository.Find(x => x.Name == updateRoleDto.Name && x.Id != id, cancellationToken);
                if (existingRole != null)
                    throw new InvalidOperationException("Role with this name already exists");
            }

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateRoleDto, role);

            // Ensure UpdatedAt is set
            role.UpdatedAt = DateTime.UtcNow;

            // Update in repository
            await _roleRepository.Update(role, cancellationToken);

            // Actualizar permisos si se proporcionaron
            if (updateRoleDto.PermissionIds != null)
            {
                await UpdateRolePermissions(id, updateRoleDto.PermissionIds, cancellationToken);
            }

            // Map Entity to DTO using AutoMapper
            var roleDto = _mapper.Map<RoleDto>(role);

            // Cargar permisos actuales para incluir en la respuesta
            roleDto.Permissions = await LoadRolePermissions(id, cancellationToken);

            return roleDto;
        }

        /// <summary>
        /// Actualiza los permisos de un rol (reemplaza los existentes)
        /// </summary>
        private async Task UpdateRolePermissions(Guid roleId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            // Obtener permisos actuales del rol
            var currentRolePermissions = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(roleId, cancellationToken);

            // Eliminar permisos actuales
            foreach (var currentRolePermission in currentRolePermissions)
            {
                await _rolePermissionRepository.Delete(currentRolePermission, cancellationToken);
            }

            // Asignar nuevos permisos
            foreach (var permissionId in permissionIds)
            {
                var permission = await _permissionRepository.Find(p => p.Id == permissionId, cancellationToken);
                if (permission != null)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    };

                    await _rolePermissionRepository.Create(rolePermission, cancellationToken);
                }
            }
        }

        private async Task<List<PermissionDto>> LoadRolePermissions(Guid roleId, CancellationToken cancellationToken)
        {
            var rolePermissionRelations = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(roleId, cancellationToken);
            var permissionIds = rolePermissionRelations.Select(rp => rp.PermissionId).ToList();

            var permissions = new List<Permission>();
            foreach (var permissionId in permissionIds)
            {
                var permission = await _permissionRepository.Find(p => p.Id == permissionId, cancellationToken);
                if (permission != null)
                {
                    permissions.Add(permission);
                }
            }

            return _mapper.Map<List<PermissionDto>>(permissions);
        }
    }
}
