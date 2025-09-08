using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Commands.FinancialTransactions
{
    public class UpdateFinancialTransaction
    {
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;
        private readonly IMapper _mapper;

        public UpdateFinancialTransaction(IRepositoryBase<FinancialTransaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<FinancialTransactionDto> HandleAsync(int id, UpdateFinancialTransactionDto updateTransactionDto, CancellationToken cancellationToken)
        {
            // Find existing transaction
            var transaction = await _transactionRepository.Find(x => x.Id == id, cancellationToken);
            if (transaction == null)
                throw new KeyNotFoundException("Financial transaction not found");

            // Validations for non-null fields
            if (!string.IsNullOrWhiteSpace(updateTransactionDto.TransactionType) && string.IsNullOrWhiteSpace(updateTransactionDto.TransactionType.Trim()))
                throw new ArgumentException("Transaction type cannot be empty");

            if (updateTransactionDto.Amount.HasValue && updateTransactionDto.Amount.Value <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            if (updateTransactionDto.AccountId.HasValue && updateTransactionDto.AccountId.Value <= 0)
                throw new ArgumentException("Valid Account ID is required");

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateTransactionDto, transaction);

            // Update in repository
            await _transactionRepository.Update(transaction, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<FinancialTransactionDto>(transaction);
        }
    }
}
