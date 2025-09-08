using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public interface IRoleQueryHandler
    {
        Task<RoleDto?> GetRoleById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<RoleDto>> GetAllRoles(CancellationToken cancellationToken);
    }
}
