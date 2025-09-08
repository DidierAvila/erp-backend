using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.FinancialTransactions
{
    public class GetFinancialTransactionById
    {
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;
        private readonly IMapper _mapper;

        public GetFinancialTransactionById(IRepositoryBase<FinancialTransaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<FinancialTransactionDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByID(id, cancellationToken);
            if (transaction == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<FinancialTransactionDto>(transaction);
        }
    }
}
