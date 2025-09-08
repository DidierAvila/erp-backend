using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.StockMovements
{
    public class GetStockMovementsByProduct
    {
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;
        private readonly IMapper _mapper;

        public GetStockMovementsByProduct(IRepositoryBase<StockMovement> stockMovementRepository, IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockMovementDto>> HandleAsync(int productId, CancellationToken cancellationToken)
        {
            var stockMovements = await _stockMovementRepository.Finds(x => x.ProductId == productId, cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<StockMovementDto>>(stockMovements ?? new List<StockMovement>());
        }
    }
}
