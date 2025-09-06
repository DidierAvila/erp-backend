namespace ERP.Domain.DTOs.Finance
{
    public class FinancialTransactionDto
    {
        public int Id { get; set; }
        public string TransactionType { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string? Description { get; set; }
        public int AccountId { get; set; }
        public string? AccountType { get; set; }
    }

    public class CreateFinancialTransactionDto
    {
        public string TransactionType { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string? Description { get; set; }
        public int AccountId { get; set; }
    }

    public class UpdateFinancialTransactionDto
    {
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateOnly? TransactionDate { get; set; }
        public string? Description { get; set; }
        public int? AccountId { get; set; }
    }

    public class FinancialSummaryDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetBalance { get; set; }
        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd { get; set; }
    }

    public class TransactionReportDto
    {
        public string TransactionType { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
        public DateOnly? LastTransaction { get; set; }
    }
}
