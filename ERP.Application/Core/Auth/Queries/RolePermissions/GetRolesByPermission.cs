using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.RolePermissions
{
    public class GetRolesByPermission
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;

        public GetRolesByPermission(IRolePermissionRepository rolePermissionRepository, IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolePermissionDto>> HandleAsync(Guid permissionId, CancellationToken cancellationToken = default)
        {
            var entities = await _rolePermissionRepository.GetRolesByPermissionIdAsync(permissionId, cancellationToken);
            return _mapper.Map<IEnumerable<RolePermissionDto>>(entities);
        }
    }
}
