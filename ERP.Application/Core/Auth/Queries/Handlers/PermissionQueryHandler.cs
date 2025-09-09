using ERP.Application.Core.Auth.Queries.Permissions;
using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class PermissionQueryHandler : IPermissionQueryHandler
    {
        private readonly GetPermissionById _getPermissionById;
        private readonly GetAllPermissions _getAllPermissions;
        private readonly GetActivePermissions _getActivePermissions;
        private readonly GetPermissionsSummary _getPermissionsSummary;

        public PermissionQueryHandler(
            GetPermissionById getPermissionById,
            GetAllPermissions getAllPermissions,
            GetActivePermissions getActivePermissions,
            GetPermissionsSummary getPermissionsSummary)
        {
            _getPermissionById = getPermissionById;
            _getAllPermissions = getAllPermissions;
            _getActivePermissions = getActivePermissions;
            _getPermissionsSummary = getPermissionsSummary;
        }

        public async Task<PermissionDto?> GetPermissionById(Guid id, CancellationToken cancellationToken)
        {
            return await _getPermissionById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissions(CancellationToken cancellationToken)
        {
            return await _getAllPermissions.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<PermissionDto>> GetActivePermissions(CancellationToken cancellationToken)
        {
            return await _getActivePermissions.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<PermissionSummaryDto>> GetPermissionsSummary(CancellationToken cancellationToken)
        {
            return await _getPermissionsSummary.HandleAsync(cancellationToken);
        }
    }
}
