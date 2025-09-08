using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Commands.Products
{
    public class UpdateProduct
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public UpdateProduct(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> HandleAsync(int id, UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            // Find existing product
            var product = await _productRepository.Find(x => x.Id == id, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            // Validations
            if (string.IsNullOrWhiteSpace(updateProductDto.ProductName))
                throw new ArgumentException("Product name is required");

            if (string.IsNullOrWhiteSpace(updateProductDto.UnitOfMeasure))
                throw new ArgumentException("Unit of measure is required");

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateProductDto, product);

            // Update in repository
            await _productRepository.Update(product, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<ProductDto>(product);
        }
    }
}
