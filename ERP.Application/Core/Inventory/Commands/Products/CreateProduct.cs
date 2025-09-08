using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Commands.Products
{
    public class CreateProduct
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public CreateProduct(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> HandleAsync(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createProductDto.ProductName))
                throw new ArgumentException("Product name is required");

            if (string.IsNullOrWhiteSpace(createProductDto.Sku))
                throw new ArgumentException("SKU is required");

            if (string.IsNullOrWhiteSpace(createProductDto.UnitOfMeasure))
                throw new ArgumentException("Unit of measure is required");

            // Check if SKU already exists
            var existingProduct = await _productRepository.Find(x => x.Sku == createProductDto.Sku, cancellationToken);
            if (existingProduct != null)
                throw new InvalidOperationException("Product with this SKU already exists");

            // Map DTO to Entity using AutoMapper
            var product = _mapper.Map<Product>(createProductDto);

            // Create product in repository
            var createdProduct = await _productRepository.Create(product, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<ProductDto>(createdProduct);
        }
    }
}
