using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public interface IRoleQueryHandler
    {
        Task<RoleDto?> GetRoleById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<RoleDto>> GetAllRoles(CancellationToken cancellationToken);
        Task<PaginationResponseDto<RoleListResponseDto>> GetAllRolesFiltered(RoleFilterDto filter, CancellationToken cancellationToken);
    }
}
