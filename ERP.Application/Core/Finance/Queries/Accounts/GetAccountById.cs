using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.Accounts
{
    public class GetAccountById
    {
        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountById(IRepositoryBase<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByID(id, cancellationToken);
            if (account == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<AccountDto>(account);
        }
    }
}
