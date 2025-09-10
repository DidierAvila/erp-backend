using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.Roles
{
    public class GetRoleById
    {
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Role> _roleRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.Permission> _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;

        public GetRoleById(
            IRepositoryBase<ERP.Domain.Entities.Auth.Role> roleRepository, 
            IRepositoryBase<ERP.Domain.Entities.Auth.Permission> permissionRepository,
            IRolePermissionRepository rolePermissionRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto?> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Find(x => x.Id == id, cancellationToken);
            if (role == null)
                return null;

            // Obtener los permisos asociados al rol a través de la tabla intermedia RolePermission
            var rolePermissionRelations = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(id, cancellationToken);
            var permissionIds = rolePermissionRelations.Select(rp => rp.PermissionId).ToList();

            // Obtener los permisos completos
            List<ERP.Domain.Entities.Auth.Permission> permissions = new();
            foreach (var permissionId in permissionIds)
            {
                var permission = await _permissionRepository.Find(p => p.Id == permissionId, cancellationToken);
                if (permission != null)
                {
                    permissions.Add(permission);
                }
            }

            // Map Entity to DTO using AutoMapper
            var roleDto = _mapper.Map<RoleDto>(role);
            
            // Asignar los permisos manualmente
            roleDto.Permissions = _mapper.Map<List<PermissionDto>>(permissions);

            return roleDto;
        }
    }
}
