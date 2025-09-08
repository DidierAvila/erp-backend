using ERP.Application.Core.Inventory.Queries.StockMovements;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Queries.Handlers
{
    public class StockMovementQueryHandler : IStockMovementQueryHandler
    {
        private readonly GetStockMovementById _getStockMovementById;
        private readonly GetAllStockMovements _getAllStockMovements;
        private readonly GetStockMovementsByProduct _getStockMovementsByProduct;
        private readonly GetStockMovementsByType _getStockMovementsByType;

        public StockMovementQueryHandler(
            GetStockMovementById getStockMovementById,
            GetAllStockMovements getAllStockMovements,
            GetStockMovementsByProduct getStockMovementsByProduct,
            GetStockMovementsByType getStockMovementsByType)
        {
            _getStockMovementById = getStockMovementById;
            _getAllStockMovements = getAllStockMovements;
            _getStockMovementsByProduct = getStockMovementsByProduct;
            _getStockMovementsByType = getStockMovementsByType;
        }

        public async Task<StockMovementDto?> GetStockMovementById(int id, CancellationToken cancellationToken)
        {
            return await _getStockMovementById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<StockMovementDto>> GetAllStockMovements(CancellationToken cancellationToken)
        {
            return await _getAllStockMovements.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<StockMovementDto>> GetStockMovementsByProduct(int productId, CancellationToken cancellationToken)
        {
            return await _getStockMovementsByProduct.HandleAsync(productId, cancellationToken);
        }

        public async Task<IEnumerable<StockMovementDto>> GetStockMovementsByType(string movementType, CancellationToken cancellationToken)
        {
            return await _getStockMovementsByType.HandleAsync(movementType, cancellationToken);
        }
    }
}
