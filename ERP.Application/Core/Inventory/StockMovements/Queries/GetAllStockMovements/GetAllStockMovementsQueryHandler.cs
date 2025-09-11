using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Queries.GetAllStockMovements
{
    public class GetAllStockMovementsQueryHandler : IRequestHandler<GetAllStockMovementsQuery, ResponseDto<IEnumerable<StockMovementDto>>>
    {
        private readonly IRepositoryBase<StockMovement> _repository;
        private readonly IMapper _mapper;

        public GetAllStockMovementsQueryHandler(IRepositoryBase<StockMovement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<StockMovementDto>>> Handle(GetAllStockMovementsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stockMovements = await _repository.GetAll(cancellationToken);
                var stockMovementDtos = _mapper.Map<IEnumerable<StockMovementDto>>(stockMovements);
                
                return ResponseDto<IEnumerable<StockMovementDto>>.Success(stockMovementDtos, "Movimientos de stock obtenidos exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<StockMovementDto>>.Error($"Error al obtener los movimientos de stock: {ex.Message}");
            }
        }
    }
}