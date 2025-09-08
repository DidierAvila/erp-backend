using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Commands.StockMovements
{
    public class DeleteStockMovement
    {
        private readonly IRepositoryBase<StockMovement> _stockMovementRepository;

        public DeleteStockMovement(IRepositoryBase<StockMovement> stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            // Find existing stock movement
            var stockMovement = await _stockMovementRepository.Find(x => x.Id == id, cancellationToken);
            if (stockMovement == null)
                throw new KeyNotFoundException("Stock movement not found");

            // Business rule: Only allow deletion of recent movements (within last 24 hours)
            // This prevents data integrity issues
            if (stockMovement.MovementDate.HasValue && 
                DateTime.UtcNow - stockMovement.MovementDate.Value > TimeSpan.FromHours(24))
            {
                throw new InvalidOperationException("Cannot delete stock movements older than 24 hours");
            }

            // Delete stock movement from repository
            await _stockMovementRepository.Delete(stockMovement, cancellationToken);

            return true;
        }
    }
}
