using ERP.Domain.DTOs.Common;

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

    public class ProductFilterDto : PaginationRequestDto
    {
        /// <summary>
        /// Búsqueda general en nombre del producto, SKU y descripción
        /// </summary>
        public string? Search { get; set; }
        
        /// <summary>
        /// Filtrar por nombre específico del producto
        /// </summary>
        public string? ProductName { get; set; }
        
        /// <summary>
        /// Filtrar por SKU específico del producto
        /// </summary>
        public string? Sku { get; set; }
        
        /// <summary>
        /// Filtrar por descripción del producto
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Filtrar por unidad de medida
        /// </summary>
        public string? UnitOfMeasure { get; set; }
        
        /// <summary>
        /// Filtrar productos con stock actual mayor o igual a este valor
        /// </summary>
        public int? MinCurrentStock { get; set; }
        
        /// <summary>
        /// Filtrar productos con stock actual menor o igual a este valor
        /// </summary>
        public int? MaxCurrentStock { get; set; }
        
        /// <summary>
        /// Filtrar productos con stock mínimo mayor o igual a este valor
        /// </summary>
        public int? MinMinimumStock { get; set; }
        
        /// <summary>
        /// Filtrar productos con stock mínimo menor o igual a este valor
        /// </summary>
        public int? MaxMinimumStock { get; set; }
        
        /// <summary>
        /// Filtrar solo productos con stock bajo (CurrentStock <= MinimumStock)
        /// </summary>
        public bool? IsLowStock { get; set; }
    }
}
