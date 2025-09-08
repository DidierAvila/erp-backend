using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.Products
{
    public class GetLowStockProducts
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetLowStockProducts(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductStockDto>> HandleAsync(CancellationToken cancellationToken)
        {
            // For now, we'll get all products and filter in memory
            // In production, you'd want to push this logic to the database
            var products = await _productRepository.GetAll(cancellationToken);

            // Map to ProductStockDto to access IsLowStock property
            var productStockDtos = _mapper.Map<IEnumerable<ProductStockDto>>(products);

            // Filter low stock products
            return productStockDtos.Where(p => p.IsLowStock);
        }
    }
}
