using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Commands.Handlers
{
    public interface IProductCommandHandler
    {
        Task<ProductDto> CreateProduct(CreateProductDto command, CancellationToken cancellationToken);
        Task<ProductDto> UpdateProduct(int id, UpdateProductDto command, CancellationToken cancellationToken);
        Task<bool> DeleteProduct(int id, CancellationToken cancellationToken);
    }
}
