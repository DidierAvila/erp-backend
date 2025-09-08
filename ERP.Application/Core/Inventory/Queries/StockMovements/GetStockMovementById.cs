using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.StockMovements
{
    public class GetStockMovementById
    {
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;
        private readonly IMapper _mapper;

        public GetStockMovementById(IRepositoryBase<StockMovement> stockMovementRepository, IMapper mapper)
        {
            _stockMovementRepository = stockMovementRepository;
            _mapper = mapper;
        }

        public async Task<StockMovementDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var stockMovement = await _stockMovementRepository.GetByID(id, cancellationToken);
            if (stockMovement == null)
                return null;

            // Map Entity to DTO using AutoMapper
            return _mapper.Map<StockMovementDto>(stockMovement);
        }
    }
}
