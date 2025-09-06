namespace ERP.Domain.DTOs.Inventory
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public string? Description { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
    }

    public class CreateProductDto
    {
        public string ProductName { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public string? Description { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
    }

    public class UpdateProductDto
    {
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
    }

    public class ProductStockDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public bool IsLowStock => CurrentStock <= MinimumStock;
    }
}
