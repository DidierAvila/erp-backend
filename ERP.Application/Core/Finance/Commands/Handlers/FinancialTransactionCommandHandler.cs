using ERP.Application.Core.Finance.Commands.FinancialTransactions;
using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Commands.Handlers
{
    public class FinancialTransactionCommandHandler : IFinancialTransactionCommandHandler
    {
        private readonly CreateFinancialTransaction _createFinancialTransaction;
        private readonly UpdateFinancialTransaction _updateFinancialTransaction;
        private readonly DeleteFinancialTransaction _deleteFinancialTransaction;

        public FinancialTransactionCommandHandler(
            CreateFinancialTransaction createFinancialTransaction,
            UpdateFinancialTransaction updateFinancialTransaction,
            DeleteFinancialTransaction deleteFinancialTransaction)
        {
            _createFinancialTransaction = createFinancialTransaction;
            _updateFinancialTransaction = updateFinancialTransaction;
            _deleteFinancialTransaction = deleteFinancialTransaction;
        }

        public async Task<FinancialTransactionDto> CreateFinancialTransaction(CreateFinancialTransactionDto command, CancellationToken cancellationToken)
        {
            return await _createFinancialTransaction.HandleAsync(command, cancellationToken);
        }

        public async Task<FinancialTransactionDto> UpdateFinancialTransaction(int id, UpdateFinancialTransactionDto command, CancellationToken cancellationToken)
        {
            return await _updateFinancialTransaction.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteFinancialTransaction(int id, CancellationToken cancellationToken)
        {
            return await _deleteFinancialTransaction.HandleAsync(id, cancellationToken);
        }
    }
}
