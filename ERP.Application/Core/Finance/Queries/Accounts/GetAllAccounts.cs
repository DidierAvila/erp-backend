using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.Accounts
{
    public class GetAllAccounts
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IMapper _mapper;

        public GetAllAccounts(IRepositoryBase<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAll(cancellationToken);

            // Map collection of Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }
    }
}
