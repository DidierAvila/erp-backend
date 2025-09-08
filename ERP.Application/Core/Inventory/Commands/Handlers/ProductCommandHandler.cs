using ERP.Application.Core.Inventory.Commands.Products;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Commands.Handlers
{
    public class ProductCommandHandler : IProductCommandHandler
    {
        private readonly CreateProduct _createProduct;
        private readonly UpdateProduct _updateProduct;
        private readonly DeleteProduct _deleteProduct;

        public ProductCommandHandler(CreateProduct createProduct, UpdateProduct updateProduct, DeleteProduct deleteProduct)
        {
            _createProduct = createProduct;
            _updateProduct = updateProduct;
            _deleteProduct = deleteProduct;
        }

        public async Task<ProductDto> CreateProduct(CreateProductDto command, CancellationToken cancellationToken)
        {
            return await _createProduct.HandleAsync(command, cancellationToken);
        }

        public async Task<ProductDto> UpdateProduct(int id, UpdateProductDto command, CancellationToken cancellationToken)
        {
            return await _updateProduct.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            return await _deleteProduct.HandleAsync(id, cancellationToken);
        }
    }
}
