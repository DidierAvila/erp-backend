using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.Accounts
{
    public class GetAccountsByType
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountsByType(IRepositoryBase<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> HandleAsync(string accountType, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(accountType))
                throw new ArgumentException("Account type is required");

            var accounts = await _accountRepository.Finds(x => x.AccountType == accountType && x.IsActive, cancellationToken);

            // Map collection of Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }
    }
}
