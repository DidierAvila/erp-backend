using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Commands.Accounts
{
    public class CreateAccount
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IMapper _mapper;

        public CreateAccount(IRepositoryBase<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto> HandleAsync(CreateAccountDto createAccountDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createAccountDto.AccountName))
                throw new ArgumentException("Account name is required");

            if (string.IsNullOrWhiteSpace(createAccountDto.AccountNumber))
                throw new ArgumentException("Account number is required");

            if (string.IsNullOrWhiteSpace(createAccountDto.AccountType))
                throw new ArgumentException("Account type is required");

            // Check if account number already exists
            var existingAccount = await _accountRepository.Find(x => x.AccountNumber == createAccountDto.AccountNumber, cancellationToken);
            if (existingAccount != null)
                throw new InvalidOperationException("An account with this number already exists");

            // Map DTO to Entity using AutoMapper
            var account = _mapper.Map<Account>(createAccountDto);

            // Create account in repository
            var createdAccount = await _accountRepository.Create(account, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<AccountDto>(createdAccount);
        }
    }
}
