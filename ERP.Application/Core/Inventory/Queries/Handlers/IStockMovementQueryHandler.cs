using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Queries.Handlers
{
    public interface IStockMovementQueryHandler
    {
        Task<StockMovementDto?> GetStockMovementById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<StockMovementDto>> GetAllStockMovements(CancellationToken cancellationToken);
        Task<IEnumerable<StockMovementDto>> GetStockMovementsByProduct(int productId, CancellationToken cancellationToken);
        Task<IEnumerable<StockMovementDto>> GetStockMovementsByType(string movementType, CancellationToken cancellationToken);
    }
}
