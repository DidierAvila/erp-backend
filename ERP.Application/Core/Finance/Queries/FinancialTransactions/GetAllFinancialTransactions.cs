using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.FinancialTransactions
{
    public class GetAllFinancialTransactions
    {
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;
        private readonly IMapper _mapper;

        public GetAllFinancialTransactions(IRepositoryBase<FinancialTransaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FinancialTransactionDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetAll(cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<FinancialTransactionDto>>(transactions);
        }
    }
}
