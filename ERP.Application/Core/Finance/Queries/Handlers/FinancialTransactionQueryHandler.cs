using ERP.Application.Core.Finance.Queries.FinancialTransactions;
using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Queries.Handlers
{
    public class FinancialTransactionQueryHandler : IFinancialTransactionQueryHandler
    {
        private readonly GetFinancialTransactionById _getFinancialTransactionById;
        private readonly GetAllFinancialTransactions _getAllFinancialTransactions;
        private readonly GetTransactionsByType _getTransactionsByType;
        private readonly GetFinancialSummary _getFinancialSummary;

        public FinancialTransactionQueryHandler(
            GetFinancialTransactionById getFinancialTransactionById,
            GetAllFinancialTransactions getAllFinancialTransactions,
            GetTransactionsByType getTransactionsByType,
            GetFinancialSummary getFinancialSummary)
        {
            _getFinancialTransactionById = getFinancialTransactionById;
            _getAllFinancialTransactions = getAllFinancialTransactions;
            _getTransactionsByType = getTransactionsByType;
            _getFinancialSummary = getFinancialSummary;
        }

        public async Task<FinancialTransactionDto?> GetFinancialTransactionById(int id, CancellationToken cancellationToken)
        {
            return await _getFinancialTransactionById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<FinancialTransactionDto>> GetAllFinancialTransactions(CancellationToken cancellationToken)
        {
            return await _getAllFinancialTransactions.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<FinancialTransactionDto>> GetTransactionsByType(string transactionType, CancellationToken cancellationToken)
        {
            return await _getTransactionsByType.HandleAsync(transactionType, cancellationToken);
        }

        public async Task<FinancialSummaryDto> GetFinancialSummary(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
        {
            return await _getFinancialSummary.HandleAsync(startDate, endDate, cancellationToken);
        }
    }
}
