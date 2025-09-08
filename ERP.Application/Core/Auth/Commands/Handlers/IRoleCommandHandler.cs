using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public interface IRoleCommandHandler
    {
        Task<RoleDto> CreateRole(CreateRoleDto command, CancellationToken cancellationToken);
        Task<RoleDto> UpdateRole(Guid id, UpdateRoleDto command, CancellationToken cancellationToken);
        Task<bool> DeleteRole(Guid id, CancellationToken cancellationToken);
    }
}
