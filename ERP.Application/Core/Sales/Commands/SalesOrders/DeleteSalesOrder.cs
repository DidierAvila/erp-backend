using ERP.Domain.Entities.Sales;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Commands.SalesOrders
{
    public class DeleteSalesOrder
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;

        public DeleteSalesOrder(IRepositoryBase<SalesOrder> salesOrderRepository)
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            // Get existing sales order
            var existingSalesOrder = await _salesOrderRepository.GetByID(id, cancellationToken);
            if (existingSalesOrder == null)
                return false; // Sales order not found

            // Check if sales order has associated invoices
            if (existingSalesOrder.Invoices != null && existingSalesOrder.Invoices.Any())
                throw new InvalidOperationException("Cannot delete sales order with associated invoices");

            // Check if sales order is in a state that allows deletion
            var allowedStatusesForDeletion = new[] { "Draft", "Pending", "Cancelled" };
            if (!allowedStatusesForDeletion.Contains(existingSalesOrder.Status))
                throw new InvalidOperationException($"Cannot delete sales order with status '{existingSalesOrder.Status}'");

            // Delete sales order
            await _salesOrderRepository.Delete(existingSalesOrder, cancellationToken);

            return true;
        }
    }
}
