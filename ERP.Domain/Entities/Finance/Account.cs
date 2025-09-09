using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Entities.Finance
{
    [Table(name: "Accounts", Schema = "Finance")]
    public partial class Account
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; } = [];
    }
}
