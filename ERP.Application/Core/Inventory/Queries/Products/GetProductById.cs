using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.Products
{
    public class GetProductById
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetProductById(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByID(id, cancellationToken);
            if (product == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<ProductDto>(product);
        }
    }
}
