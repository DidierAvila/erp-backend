namespace ERP.Domain.DTOs.Purchases
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public List<PurchaseOrderItemDto>? Items { get; set; }
    }

    public class CreatePurchaseOrderDto
    {
        public int SupplierId { get; set; }
        public string Status { get; set; } = null!;
        public List<CreatePurchaseOrderItemDto> Items { get; set; } = new();
    }

    public class UpdatePurchaseOrderDto
    {
        public int SupplierId { get; set; }
        public string Status { get; set; } = null!;
        public List<UpdatePurchaseOrderItemDto>? Items { get; set; }
    }

    public class PurchaseOrderSummaryDto
    {
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public string SupplierName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = null!;
        public int ItemCount { get; set; }
    }
}
