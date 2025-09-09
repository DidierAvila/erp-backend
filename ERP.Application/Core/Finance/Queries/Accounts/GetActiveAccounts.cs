using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.Accounts
{
    public class GetActiveAccounts
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IMapper _mapper;

        public GetActiveAccounts(IRepositoryBase<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountSummaryDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.Finds(x => x.IsActive, cancellationToken);

            // Map collection of Entities to Summary DTOs using AutoMapper
            return _mapper.Map<IEnumerable<AccountSummaryDto>>(accounts);
        }
    }
}
