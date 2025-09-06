using ERP.Application.Core.Auth.Queries.Roles;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class RoleQueryHandler
    {
        private readonly GetRoleById _getRoleById;

        public RoleQueryHandler(GetRoleById getRoleById)
        {
            _getRoleById = getRoleById;
        }

        public async Task<RoleDto?> GetRoleById(Guid query, CancellationToken cancellationToken)
        {
            return await _getRoleById.HandleAsync(query, cancellationToken);
        }
    }
}
