using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.Products
{
    public class GetProductsBySku
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetProductsBySku(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto?> HandleAsync(string sku, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Find(x => x.Sku == sku, cancellationToken);
            if (product == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<ProductDto>(product);
        }
    }
}
