using ERP.Application.Core.Inventory.Queries.Products;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Queries.Handlers
{
    public class ProductQueryHandler : IProductQueryHandler
    {
        private readonly GetProductById _getProductById;
        private readonly GetProductsBySku _getProductsBySku;
        private readonly GetAllProducts _getAllProducts;
        private readonly GetLowStockProducts _getLowStockProducts;

        public ProductQueryHandler(
            GetProductById getProductById, 
            GetProductsBySku getProductsBySku,
            GetAllProducts getAllProducts,
            GetLowStockProducts getLowStockProducts)
        {
            _getProductById = getProductById;
            _getProductsBySku = getProductsBySku;
            _getAllProducts = getAllProducts;
            _getLowStockProducts = getLowStockProducts;
        }

        public async Task<ProductDto?> GetProductById(int id, CancellationToken cancellationToken)
        {
            return await _getProductById.HandleAsync(id, cancellationToken);
        }

        public async Task<ProductDto?> GetProductBySku(string sku, CancellationToken cancellationToken)
        {
            return await _getProductsBySku.HandleAsync(sku, cancellationToken);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts(CancellationToken cancellationToken)
        {
            return await _getAllProducts.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductStockDto>> GetLowStockProducts(CancellationToken cancellationToken)
        {
            return await _getLowStockProducts.HandleAsync(cancellationToken);
        }
    }
}
