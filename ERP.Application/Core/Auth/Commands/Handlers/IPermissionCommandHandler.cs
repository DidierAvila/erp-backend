using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public interface IPermissionCommandHandler
    {
        Task<PermissionDto> CreatePermission(CreatePermissionDto command, CancellationToken cancellationToken);
        Task<PermissionDto> UpdatePermission(Guid id, UpdatePermissionDto command, CancellationToken cancellationToken);
        Task<bool> DeletePermission(Guid id, CancellationToken cancellationToken);
    }
}
