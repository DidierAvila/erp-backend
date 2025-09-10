using ERP.Application.Core.Auth.Queries.Roles;
using ERP.Application.Core.Auth.Queries.Role;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class RoleQueryHandler : IRoleQueryHandler
    {
        private readonly GetRoleById _getRoleById;
        private readonly GetAllRoles _getAllRoles;
        private readonly GetAllRolesFiltered _getAllRolesFiltered;

        public RoleQueryHandler(
            GetRoleById getRoleById, 
            GetAllRoles getAllRoles,
            GetAllRolesFiltered getAllRolesFiltered)
        {
            _getRoleById = getRoleById;
            _getAllRoles = getAllRoles;
            _getAllRolesFiltered = getAllRolesFiltered;
        }

        public async Task<RoleDto?> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            return await _getRoleById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles(CancellationToken cancellationToken)
        {
            return await _getAllRoles.HandleAsync(cancellationToken);
        }

        public async Task<PaginationResponseDto<RoleListResponseDto>> GetAllRolesFiltered(RoleFilterDto filter, CancellationToken cancellationToken)
        {
            return await _getAllRolesFiltered.GetRolesFiltered(filter, cancellationToken);
        }
    }
}
