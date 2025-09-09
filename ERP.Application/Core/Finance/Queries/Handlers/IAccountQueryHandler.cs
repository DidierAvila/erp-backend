using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Queries.Handlers
{
    public interface IAccountQueryHandler
    {
        Task<AccountDto?> GetAccountById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken);
        Task<IEnumerable<AccountDto>> GetAccountsByType(string accountType, CancellationToken cancellationToken);
        Task<IEnumerable<AccountSummaryDto>> GetActiveAccounts(CancellationToken cancellationToken);
    }
}
