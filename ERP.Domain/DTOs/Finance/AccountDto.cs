namespace ERP.Domain.DTOs.Finance
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateAccountDto
    {
        public string AccountName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Balance { get; set; } = 0m;
        public bool IsActive { get; set; } = true;
    }

    public class UpdateAccountDto
    {
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? Description { get; set; }
        public decimal? Balance { get; set; }
        public bool? IsActive { get; set; }
    }

    public class AccountSummaryDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
