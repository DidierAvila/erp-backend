using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Queries.Handlers
{
    public interface IProductQueryHandler
    {
        Task<ProductDto?> GetProductById(int id, CancellationToken cancellationToken);
        Task<ProductDto?> GetProductBySku(string sku, CancellationToken cancellationToken);
        Task<IEnumerable<ProductDto>> GetAllProducts(CancellationToken cancellationToken);
        Task<PaginationResponseDto<ProductDto>> GetAllProducts(ProductFilterDto filter, CancellationToken cancellationToken = default);
        Task<PaginationResponseDto<ProductDto>> GetAllProducts(int page, int pageSize, string? sortBy, string? productName, CancellationToken cancellationToken);
        Task<IEnumerable<ProductStockDto>> GetLowStockProducts(CancellationToken cancellationToken);
    }
}
