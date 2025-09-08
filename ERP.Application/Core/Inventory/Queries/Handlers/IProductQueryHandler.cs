using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Queries.Handlers
{
    public interface IProductQueryHandler
    {
        Task<ProductDto?> GetProductById(int id, CancellationToken cancellationToken);
        Task<ProductDto?> GetProductBySku(string sku, CancellationToken cancellationToken);
        Task<IEnumerable<ProductDto>> GetAllProducts(CancellationToken cancellationToken);
        Task<IEnumerable<ProductStockDto>> GetLowStockProducts(CancellationToken cancellationToken);
    }
}
