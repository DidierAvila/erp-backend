namespace ERP.Domain.DTOs.Inventory
{
    public class StockMovementDto
    {
        public int Id { get; set; }
        public string MovementType { get; set; } = null!;
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? FromLocationId { get; set; }
        public string? FromLocationName { get; set; }
        public int? ToLocationId { get; set; }
        public string? ToLocationName { get; set; }
        public int Quantity { get; set; }
        public DateTime? MovementDate { get; set; }
        public string? Notes { get; set; }
    }

    public class CreateStockMovementDto
    {
        public string MovementType { get; set; } = null!;
        public int ProductId { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }

    public class UpdateStockMovementDto
    {
        public string MovementType { get; set; } = null!;
        public int ProductId { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }

    public class StockMovementReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string MovementType { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public DateTime? LastMovement { get; set; }
    }
}
