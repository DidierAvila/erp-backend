using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.StockMovements
{
    public class GetStockMovementsByType
    {
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;
        private readonly IMapper _mapper;

        public GetStockMovementsByType(IRepositoryBase<StockMovement> stockMovementRepository, IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockMovementDto>> HandleAsync(string movementType, CancellationToken cancellationToken)
        {
            var stockMovements = await _stockMovementRepository.Finds(x => x.MovementType == movementType, cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<StockMovementDto>>(stockMovements ?? new List<StockMovement>());
        }
    }
}
