namespace ERP.Domain.DTOs.Sales
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateOnly InvoiceDate { get; set; }
        public int? SalesOrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
    }

    public class CreateInvoiceDto
    {
        public string InvoiceNumber { get; set; } = null!;
        public DateOnly InvoiceDate { get; set; }
        public int? SalesOrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
    }

    public class UpdateInvoiceDto
    {
        public string? InvoiceNumber { get; set; }
        public DateOnly? InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
    }

    public class InvoiceSummaryDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public DateOnly InvoiceDate { get; set; }
        public string CustomerName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
    }
}
