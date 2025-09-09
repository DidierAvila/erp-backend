using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Commands.Accounts
{
    public class UpdateAccount
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IMapper _mapper;

        public UpdateAccount(IRepositoryBase<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto> HandleAsync(int id, UpdateAccountDto updateAccountDto, CancellationToken cancellationToken)
        {
            // Find existing account
            var account = await _accountRepository.Find(x => x.Id == id, cancellationToken);
            if (account == null)
                throw new KeyNotFoundException("Account not found");

            // Validate account number uniqueness if it's being updated
            if (!string.IsNullOrWhiteSpace(updateAccountDto.AccountNumber) && 
                updateAccountDto.AccountNumber != account.AccountNumber)
            {
                var existingAccount = await _accountRepository.Find(x => x.AccountNumber == updateAccountDto.AccountNumber, cancellationToken);
                if (existingAccount != null)
                    throw new InvalidOperationException("An account with this number already exists");
            }

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateAccountDto, account);
            
            // Ensure UpdatedAt is set
            account.UpdatedAt = DateTime.UtcNow;

            // Update in repository
            await _accountRepository.Update(account, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<AccountDto>(account);
        }
    }
}
