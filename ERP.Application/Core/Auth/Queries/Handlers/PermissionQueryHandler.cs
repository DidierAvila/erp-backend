using ERP.Application.Core.Auth.Queries.Permissions;
using ERP.Application.Core.Auth.Queries.Permission;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public class PermissionQueryHandler : IPermissionQueryHandler
    {
        private readonly GetPermissionById _getPermissionById;
        private readonly GetAllPermissions _getAllPermissions;
        private readonly GetActivePermissions _getActivePermissions;
        private readonly GetPermissionsSummary _getPermissionsSummary;
        private readonly GetAllPermissionsFiltered _getAllPermissionsFiltered;

        public PermissionQueryHandler(
            GetPermissionById getPermissionById,
            GetAllPermissions getAllPermissions,
            GetActivePermissions getActivePermissions,
            GetPermissionsSummary getPermissionsSummary,
            GetAllPermissionsFiltered getAllPermissionsFiltered)
        {
            _getPermissionById = getPermissionById;
            _getAllPermissions = getAllPermissions;
            _getActivePermissions = getActivePermissions;
            _getPermissionsSummary = getPermissionsSummary;
            _getAllPermissionsFiltered = getAllPermissionsFiltered;
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

        public async Task<PaginationResponseDto<PermissionListResponseDto>> GetAllPermissionsFiltered(PermissionFilterDto filter, CancellationToken cancellationToken)
        {
            return await _getAllPermissionsFiltered.GetPermissionsFiltered(filter, cancellationToken);
        }
    }
}
