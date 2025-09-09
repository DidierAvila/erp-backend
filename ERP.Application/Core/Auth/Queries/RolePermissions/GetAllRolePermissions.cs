using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Queries.RolePermissions
{
    public class GetAllRolePermissions
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;

        public GetAllRolePermissions(IRolePermissionRepository rolePermissionRepository, IMapper mapper)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolePermissionDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _rolePermissionRepository.GetRolePermissionsWithDetailsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<RolePermissionDto>>(entities);
        }
    }
}
