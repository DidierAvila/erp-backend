using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Commands.FinancialTransactions
{
    public class DeleteFinancialTransaction
    {
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;

        public DeleteFinancialTransaction(IRepositoryBase<FinancialTransaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            // Find existing transaction
            var transaction = await _transactionRepository.Find(x => x.Id == id, cancellationToken);
            if (transaction == null)
                throw new KeyNotFoundException("Financial transaction not found");

            // Delete transaction from repository
            await _transactionRepository.Delete(transaction, cancellationToken);

            return true;
        }
    }
}
