using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Commands.FinancialTransactions
{
    public class CreateFinancialTransaction
    {
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;
        private readonly IMapper _mapper;

        public CreateFinancialTransaction(IRepositoryBase<FinancialTransaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<FinancialTransactionDto> HandleAsync(CreateFinancialTransactionDto createTransactionDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createTransactionDto.TransactionType))
                throw new ArgumentException("Transaction type is required");

            if (createTransactionDto.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            if (createTransactionDto.AccountId <= 0)
                throw new ArgumentException("Valid Account ID is required");

            // Map DTO to Entity using AutoMapper
            var transaction = _mapper.Map<FinancialTransaction>(createTransactionDto);
            transaction.CreatedAt = DateTime.UtcNow;

            // Create transaction in repository
            var createdTransaction = await _transactionRepository.Create(transaction, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<FinancialTransactionDto>(createdTransaction);
        }
    }
}
