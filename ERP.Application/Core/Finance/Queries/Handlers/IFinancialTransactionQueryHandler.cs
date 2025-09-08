using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Queries.Handlers
{
    public interface IFinancialTransactionQueryHandler
    {
        Task<FinancialTransactionDto?> GetFinancialTransactionById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<FinancialTransactionDto>> GetAllFinancialTransactions(CancellationToken cancellationToken);
        Task<IEnumerable<FinancialTransactionDto>> GetTransactionsByType(string transactionType, CancellationToken cancellationToken);
        Task<FinancialSummaryDto> GetFinancialSummary(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken);
    }
}
