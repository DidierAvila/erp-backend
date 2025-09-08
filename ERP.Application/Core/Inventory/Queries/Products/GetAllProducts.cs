using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.Products
{
    public class GetAllProducts
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetAllProducts(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll(cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
