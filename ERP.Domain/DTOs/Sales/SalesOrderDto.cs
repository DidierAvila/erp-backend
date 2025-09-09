namespace ERP.Domain.DTOs.Sales
{
    public class SalesOrderDto
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public List<SalesOrderItemDto>? Items { get; set; }
    }

    public class CreateSalesOrderDto
    {
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = null!;
        public List<CreateSalesOrderItemDto> Items { get; set; } = new();
    }

    public class UpdateSalesOrderDto
    {
        public Guid CustomerId { get; set; }
        public string Status { get; set; } = null!;
        public List<UpdateSalesOrderItemDto>? Items { get; set; }
    }

    public class SalesOrderSummaryDto
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public string CustomerName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public int ItemCount { get; set; }
    }
}
