using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Commands.UpdateStockMovement
{
    public class UpdateStockMovementCommandHandler : IRequestHandler<UpdateStockMovementCommand, ResponseDto<StockMovementDto>>
    {
        private readonly IRepositoryBase<StockMovement> _repository;
        private readonly IMapper _mapper;

        public UpdateStockMovementCommandHandler(IRepositoryBase<StockMovement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<StockMovementDto>> Handle(UpdateStockMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stockMovement = await _repository.GetByID(request.Id, cancellationToken);
                if (stockMovement == null)
                {
                    return ResponseDto<StockMovementDto>.Error("Movimiento de stock no encontrado.");
                }

                _mapper.Map(request.UpdateStockMovementDto, stockMovement);
                await _repository.Update(stockMovement, cancellationToken);
                
                var stockMovementDto = _mapper.Map<StockMovementDto>(stockMovement);
                
                return ResponseDto<StockMovementDto>.Success(stockMovementDto, "Movimiento de stock actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseDto<StockMovementDto>.Error($"Error al actualizar el movimiento de stock: {ex.Message}");
            }
        }
    }
}