using ERP.Application.Core.Inventory.Commands.StockMovements;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Commands.Handlers
{
    public class StockMovementCommandHandler : IStockMovementCommandHandler
    {
        private readonly CreateStockMovement _createStockMovement;
        private readonly DeleteStockMovement _deleteStockMovement;

        public StockMovementCommandHandler(CreateStockMovement createStockMovement, DeleteStockMovement deleteStockMovement)
        {
            _createStockMovement = createStockMovement;
            _deleteStockMovement = deleteStockMovement;
        }

        public async Task<StockMovementDto> CreateStockMovement(CreateStockMovementDto command, CancellationToken cancellationToken)
        {
            return await _createStockMovement.HandleAsync(command, cancellationToken);
        }

        public async Task<bool> DeleteStockMovement(int id, CancellationToken cancellationToken)
        {
            return await _deleteStockMovement.HandleAsync(id, cancellationToken);
        }
    }
}
