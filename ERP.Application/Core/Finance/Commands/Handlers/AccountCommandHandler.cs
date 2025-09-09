using ERP.Application.Core.Finance.Commands.Accounts;
using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Commands.Handlers
{
    public class AccountCommandHandler : IAccountCommandHandler
    {
        private readonly CreateAccount _createAccount;
        private readonly UpdateAccount _updateAccount;
        private readonly DeleteAccount _deleteAccount;

        public AccountCommandHandler(
            CreateAccount createAccount,
            UpdateAccount updateAccount,
            DeleteAccount deleteAccount)
        {
            _createAccount = createAccount;
            _updateAccount = updateAccount;
            _deleteAccount = deleteAccount;
        }

        public async Task<AccountDto> CreateAccount(CreateAccountDto command, CancellationToken cancellationToken)
        {
            return await _createAccount.HandleAsync(command, cancellationToken);
        }

        public async Task<AccountDto> UpdateAccount(int id, UpdateAccountDto command, CancellationToken cancellationToken)
        {
            return await _updateAccount.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteAccount(int id, CancellationToken cancellationToken)
        {
            return await _deleteAccount.HandleAsync(id, cancellationToken);
        }
    }
}
