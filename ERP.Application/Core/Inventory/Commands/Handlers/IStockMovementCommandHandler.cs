using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Commands.Handlers
{
    public interface IStockMovementCommandHandler
    {
        Task<StockMovementDto> CreateStockMovement(CreateStockMovementDto command, CancellationToken cancellationToken);
        Task<bool> DeleteStockMovement(int id, CancellationToken cancellationToken);
    }
}
