using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Commands.Products
{
    public class DeleteProduct
    {
        private readonly IRepositoryBase<Product> _productRepository;

        public DeleteProduct(IRepositoryBase<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            // Find existing product
            var product = await _productRepository.Find(x => x.Id == id, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            // Check if product has related records (you might want to add business logic here)
            // For example: if product has stock movements, orders, etc.
            
            // Delete product from repository
            await _productRepository.Delete(product, cancellationToken);

            return true;
        }
    }
}
