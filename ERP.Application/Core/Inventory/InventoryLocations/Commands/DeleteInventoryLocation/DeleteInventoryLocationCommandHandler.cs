using ERP.Domain.DTOs.Common;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;
using MediatR;

namespace ERP.Application.Core.Inventory.InventoryLocations.Commands.DeleteInventoryLocation
{
    public class DeleteInventoryLocationCommandHandler : IRequestHandler<DeleteInventoryLocationCommand, ResponseDto<bool>>
    {
        private readonly IRepositoryBase<InventoryLocation> _repository;
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;

        public DeleteInventoryLocationCommandHandler(IRepositoryBase<InventoryLocation> repository, IRepositoryBase<StockMovement> stockMovementRepository)
        {
            _repository = repository;
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<ResponseDto<bool>> Handle(DeleteInventoryLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inventoryLocation = await _repository.GetByID(request.Id, cancellationToken);
                if (inventoryLocation == null)
                {
                    return new ResponseDto<bool>
                    {
                        IsSuccess = false,
                        Message = "Ubicación de inventario no encontrada",
                        Data = false
                    };
                }

                // Validar que no existan movimientos de stock asociados
                var stockMovements = await _stockMovementRepository.Finds(x => x.FromLocationId == request.Id || x.ToLocationId == request.Id, cancellationToken);
                if (stockMovements != null && stockMovements.Any())
                {
                    return new ResponseDto<bool>
                    {
                        IsSuccess = false,
                        Message = "No se puede eliminar la ubicación porque tiene movimientos de stock asociados",
                        Data = false
                    };
                }

                await _repository.Delete(inventoryLocation, cancellationToken);

                return new ResponseDto<bool>
                {
                    IsSuccess = true,
                    Message = "Ubicación de inventario eliminada exitosamente",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = $"Error al eliminar la ubicación de inventario: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}