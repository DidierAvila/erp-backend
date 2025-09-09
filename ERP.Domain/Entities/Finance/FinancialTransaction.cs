namespace ERP.Domain.Entities.Finance;

public partial class FinancialTransaction
{
    public int Id { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly TransactionDate { get; set; }

    public string? Description { get; set; }

    public int AccountId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Account Account { get; set; } = null!;
}
