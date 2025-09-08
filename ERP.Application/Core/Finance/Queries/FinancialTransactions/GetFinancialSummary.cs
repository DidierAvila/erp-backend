using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Finance.Queries.FinancialTransactions
{
    public class GetFinancialSummary
    {
        private readonly IRepositoryBase<FinancialTransaction> _transactionRepository;
        private readonly IMapper _mapper;

        public GetFinancialSummary(IRepositoryBase<FinancialTransaction> transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<FinancialSummaryDto> HandleAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.Finds(
                x => x.TransactionDate >= startDate && x.TransactionDate <= endDate, 
                cancellationToken);

            var transactionsList = transactions?.ToList() ?? new List<FinancialTransaction>();

            // Calculate summary (in production, this should be done in the database)
            var incomeTypes = new[] { "Income", "Revenue", "Sale" }; // Define income transaction types
            var expenseTypes = new[] { "Expense", "Cost", "Purchase" }; // Define expense transaction types

            var totalIncome = transactionsList
                .Where(t => incomeTypes.Contains(t.TransactionType, StringComparer.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            var totalExpenses = transactionsList
                .Where(t => expenseTypes.Contains(t.TransactionType, StringComparer.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            return new FinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                NetBalance = totalIncome - totalExpenses,
                PeriodStart = startDate,
                PeriodEnd = endDate
            };
        }
    }
}
