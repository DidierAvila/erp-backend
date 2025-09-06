namespace ERP.Domain.DTOs.Purchases
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }

    public class CreateSupplierDto
    {
        public string SupplierName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }

    public class UpdateSupplierDto
    {
        public string SupplierName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
    }
}
