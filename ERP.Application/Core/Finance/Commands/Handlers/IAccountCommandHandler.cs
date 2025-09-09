using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Commands.Handlers
{
    public interface IAccountCommandHandler
    {
        Task<AccountDto> CreateAccount(CreateAccountDto command, CancellationToken cancellationToken);
        Task<AccountDto> UpdateAccount(int id, UpdateAccountDto command, CancellationToken cancellationToken);
        Task<bool> DeleteAccount(int id, CancellationToken cancellationToken);
    }
}
