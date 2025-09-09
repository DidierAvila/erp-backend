using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Commands.Accounts
{
    public class DeleteAccount
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;

        public DeleteAccount(IRepositoryBase<Account> accountRepository, IRepositoryBase<FinancialTransaction> transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            // Find existing account
            var account = await _accountRepository.Find(x => x.Id == id, cancellationToken);
            if (account == null)
                throw new KeyNotFoundException("Account not found");

            // Check if account has transactions
            var hasTransactions = await _transactionRepository.Finds(x => x.Account != null && x.Account.Id == id, cancellationToken);
            if (hasTransactions?.Any() == true)
                throw new InvalidOperationException("Cannot delete account with existing transactions. Please deactivate the account instead.");

            // Delete account
            await _accountRepository.Delete(account, cancellationToken);
            return true;
        }
    }
}
