using ERP.Domain.DTOs.Common;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.StockMovements.Commands.DeleteStockMovement
{
    public class DeleteStockMovementCommandHandler : IRequestHandler<DeleteStockMovementCommand, ResponseDto<bool>>
    {
        private readonly IRepositoryBase<StockMovement> _repository;

        public DeleteStockMovementCommandHandler(IRepositoryBase<StockMovement> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteStockMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stockMovement = await _repository.GetByID(request.Id, cancellationToken);
                if (stockMovement == null)
                {
                    return ResponseDto<bool>.Error("Movimiento de stock no encontrado.");
                }

                await _repository.Delete(stockMovement, cancellationToken);
                
                return ResponseDto<bool>.Success(true, "Movimiento de stock eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.Error($"Error al eliminar el movimiento de stock: {ex.Message}");
            }
        }
    }
}