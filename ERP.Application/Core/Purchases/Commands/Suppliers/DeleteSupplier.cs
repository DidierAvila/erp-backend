using ERP.Domain.Entities.Purchases;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Purchases.Commands.Suppliers
{
    public class DeleteSupplier
    {
        private readonly IRepositoryBase<Supplier> _supplierRepository;

        public DeleteSupplier(IRepositoryBase<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            // Get existing supplier
            var existingSupplier = await _supplierRepository.GetByID(id, cancellationToken);
            if (existingSupplier == null)
                return false; // Supplier not found

            // Check if supplier has associated purchase orders
            if (existingSupplier.PurchaseOrders != null && existingSupplier.PurchaseOrders.Any())
                throw new InvalidOperationException("Cannot delete supplier with associated purchase orders");

            // Delete supplier
            await _supplierRepository.Delete(existingSupplier, cancellationToken);

            return true;
        }
    }
}
