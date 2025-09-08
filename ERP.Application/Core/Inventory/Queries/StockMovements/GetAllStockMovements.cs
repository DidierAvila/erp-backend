using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.StockMovements
{
    public class GetAllStockMovements
    {
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;
        private readonly IMapper _mapper;

        public GetAllStockMovements(IRepositoryBase<StockMovement> stockMovementRepository, IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockMovementDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var stockMovements = await _stockMovementRepository.GetAll(cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<StockMovementDto>>(stockMovements);
        }
    }
}
