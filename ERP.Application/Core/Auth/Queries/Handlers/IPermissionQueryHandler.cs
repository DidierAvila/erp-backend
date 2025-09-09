using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public interface IPermissionQueryHandler
    {
        Task<PermissionDto?> GetPermissionById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<PermissionDto>> GetAllPermissions(CancellationToken cancellationToken);
        Task<IEnumerable<PermissionDto>> GetActivePermissions(CancellationToken cancellationToken);
        Task<IEnumerable<PermissionSummaryDto>> GetPermissionsSummary(CancellationToken cancellationToken);
    }
}
