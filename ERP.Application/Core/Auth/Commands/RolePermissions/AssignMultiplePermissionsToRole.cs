using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.RolePermissions
{
    public class AssignMultiplePermissionsToRole
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public AssignMultiplePermissionsToRole(
            IRolePermissionRepository rolePermissionRepository,
            IRepositoryBase<Role> roleRepository,
            IRepositoryBase<Permission> permissionRepository,
            IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<MultiplePermissionAssignmentResult> HandleAsync(AssignMultiplePermissionsToRoleDto request, CancellationToken cancellationToken = default)
        {
            var result = new MultiplePermissionAssignmentResult
            {
                RoleId = request.RoleId
            };

            // Verificar que el rol existe
            var role = await _roleRepository.Find(r => r.Id == request.RoleId, cancellationToken);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {request.RoleId} not found");
            }

            result.RoleName = role.Name;

            // Obtener los permisos existentes del rol para evitar duplicados
            var existingRolePermissions = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(request.RoleId, cancellationToken);
            var existingPermissionIds = existingRolePermissions?.Select(rp => rp.PermissionId).ToHashSet() ?? new HashSet<Guid>();

            // Procesar cada permiso individualmente
            foreach (var permissionId in request.PermissionIds)
            {
                try
                {
                    // Verificar si ya existe la asignación
                    if (existingPermissionIds.Contains(permissionId))
                    {
                        var permission = await _permissionRepository.Find(p => p.Id == permissionId, cancellationToken);
                        result.ExistingPermissions.Add(permission?.Name ?? permissionId.ToString());
                        continue;
                    }

                    // Verificar que el permiso existe
                    var permissionToAssign = await _permissionRepository.Find(p => p.Id == permissionId, cancellationToken);
                    if (permissionToAssign == null)
                    {
                        result.FailedAssignments.Add(new PermissionAssignmentError
                        {
                            PermissionId = permissionId,
                            PermissionName = permissionId.ToString(),
                            ErrorMessage = $"Permission with ID {permissionId} not found"
                        });
                        continue;
                    }

                    // Crear la asignación
                    var rolePermission = new RolePermission
                    {
                        RoleId = request.RoleId,
                        PermissionId = permissionId
                    };

                    var createdRolePermission = await _rolePermissionRepository.Create(rolePermission, cancellationToken);

                    // Agregar al resultado exitoso
                    result.SuccessfulAssignments.Add(new RolePermissionDto
                    {
                        RoleId = request.RoleId,
                        PermissionId = permissionId,
                        RoleName = role.Name,
                        PermissionName = permissionToAssign.Name
                    });
                }
                catch (Exception ex)
                {
                    var permission = await _permissionRepository.Find(p => p.Id == permissionId, cancellationToken);
                    result.FailedAssignments.Add(new PermissionAssignmentError
                    {
                        PermissionId = permissionId,
                        PermissionName = permission?.Name ?? permissionId.ToString(),
                        ErrorMessage = ex.Message
                    });
                }
            }

            return result;
        }
    }
}
