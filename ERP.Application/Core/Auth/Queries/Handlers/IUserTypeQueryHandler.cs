using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Core.Auth.Queries.Handlers
{
    public interface IUserTypeQueryHandler
    {
        Task<UserTypeDto?> GetUserTypeById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<UserTypeDto>> GetAllUserTypes(CancellationToken cancellationToken);
        Task<IEnumerable<UserTypeDto>> GetActiveUserTypes(CancellationToken cancellationToken);
        Task<IEnumerable<UserTypeSummaryDto>> GetUserTypesSummary(CancellationToken cancellationToken);
    }
}
