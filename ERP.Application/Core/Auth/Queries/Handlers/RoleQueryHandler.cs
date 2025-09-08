using ERP.Application.Core.Auth.Queries.Roles;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class RoleQueryHandler : IRoleQueryHandler
    {
        private readonly GetRoleById _getRoleById;
        private readonly GetAllRoles _getAllRoles;

        public RoleQueryHandler(GetRoleById getRoleById, GetAllRoles getAllRoles)
        {
            _getRoleById = getRoleById;
            _getAllRoles = getAllRoles;
        }

        public async Task<RoleDto?> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            return await _getRoleById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles(CancellationToken cancellationToken)
        {
            return await _getAllRoles.HandleAsync(cancellationToken);
        }
    }
}
