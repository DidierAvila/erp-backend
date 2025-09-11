using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;

namespace ERP.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Entity to DTO mappings
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductStockDto>();

            // DTO to Entity mappings
            CreateMap<CreateProductDto, Product>();
                // AutoMapper mapea automáticamente: ProductName, Sku, Description, UnitOfMeasure, CurrentStock
                // AutoMapper ignora automáticamente: Id, PurchaseOrderItems, SalesOrderItems, StockMovements

            CreateMap<UpdateProductDto, Product>();
                // AutoMapper mapea automáticamente: ProductName, Description, UnitOfMeasure, CurrentStock
                // AutoMapper ignora automáticamente: Id, Sku, PurchaseOrderItems, SalesOrderItems, StockMovements
        }
    }
}
