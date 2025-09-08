using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Commands.StockMovements
{
    public class CreateStockMovement
    {
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public CreateStockMovement(
            IRepositoryBase<StockMovement> stockMovementRepository,
            IRepositoryBase<Product> productRepository,
            IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<StockMovementDto> HandleAsync(CreateStockMovementDto createStockMovementDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createStockMovementDto.MovementType))
                throw new ArgumentException("Movement type is required");

            if (createStockMovementDto.ProductId <= 0)
                throw new ArgumentException("Valid Product ID is required");

            if (createStockMovementDto.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            // Validate product exists
            var product = await _productRepository.Find(x => x.Id == createStockMovementDto.ProductId, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            // Business logic: For transfers, both locations should be specified
            var movementType = createStockMovementDto.MovementType.ToLower();
            if (movementType == "transfer")
            {
                if (!createStockMovementDto.FromLocationId.HasValue || !createStockMovementDto.ToLocationId.HasValue)
                    throw new ArgumentException("Transfer movements require both FromLocation and ToLocation");
            }

            // Map DTO to Entity using AutoMapper
            var stockMovement = _mapper.Map<StockMovement>(createStockMovementDto);
            stockMovement.MovementDate = DateTime.UtcNow;

            // Create stock movement in repository
            var createdStockMovement = await _stockMovementRepository.Create(stockMovement, cancellationToken);

            // Update product stock based on movement type
            await UpdateProductStock(product, movementType, createStockMovementDto.Quantity, cancellationToken);

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<StockMovementDto>(createdStockMovement);
        }

        private async Task UpdateProductStock(Product product, string movementType, int quantity, CancellationToken cancellationToken)
        {
            switch (movementType)
            {
                case "in":
                case "purchase":
                case "return":
                    product.CurrentStock += quantity;
                    break;
                case "out":
                case "sale":
                case "adjustment":
                    product.CurrentStock -= quantity;
                    if (product.CurrentStock < 0)
                        throw new InvalidOperationException("Stock cannot be negative");
                    break;
                case "transfer":
                    // Transfer doesn't change total stock, just location
                    break;
                default:
                    throw new ArgumentException($"Unknown movement type: {movementType}");
            }

            await _productRepository.Update(product, cancellationToken);
        }
    }
}
