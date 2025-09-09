using ERP.Application.Core.Finance.Queries.Accounts;
using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Queries.Handlers
{
    public class AccountQueryHandler : IAccountQueryHandler
    {
        private readonly GetAccountById _getAccountById;
        private readonly GetAllAccounts _getAllAccounts;
        private readonly GetAccountsByType _getAccountsByType;
        private readonly GetActiveAccounts _getActiveAccounts;

        public AccountQueryHandler(
            GetAccountById getAccountById,
            GetAllAccounts getAllAccounts,
            GetAccountsByType getAccountsByType,
            GetActiveAccounts getActiveAccounts)
        {
            _getAccountById = getAccountById;
            _getAllAccounts = getAllAccounts;
            _getAccountsByType = getAccountsByType;
            _getActiveAccounts = getActiveAccounts;
        }

        public async Task<AccountDto?> GetAccountById(int id, CancellationToken cancellationToken)
        {
            return await _getAccountById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccounts(CancellationToken cancellationToken)
        {
            return await _getAllAccounts.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsByType(string accountType, CancellationToken cancellationToken)
        {
            return await _getAccountsByType.HandleAsync(accountType, cancellationToken);
        }

        public async Task<IEnumerable<AccountSummaryDto>> GetActiveAccounts(CancellationToken cancellationToken)
        {
            return await _getActiveAccounts.HandleAsync(cancellationToken);
        }
    }
}
