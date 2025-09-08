using ERP.Application.Core.Sales.Commands.SalesOrders;
using ERP.Domain.DTOs.Sales;

namespace ERP.Application.Core.Sales.Commands.Handlers
{
    public class SalesOrderCommandHandler : ISalesOrderCommandHandler
    {
        private readonly CreateSalesOrder _createSalesOrder;
        private readonly UpdateSalesOrder _updateSalesOrder;
        private readonly DeleteSalesOrder _deleteSalesOrder;

        public SalesOrderCommandHandler(CreateSalesOrder createSalesOrder, UpdateSalesOrder updateSalesOrder, DeleteSalesOrder deleteSalesOrder)
        {
            _createSalesOrder = createSalesOrder;
            _updateSalesOrder = updateSalesOrder;
            _deleteSalesOrder = deleteSalesOrder;
        }

        public async Task<SalesOrderDto> CreateSalesOrder(CreateSalesOrderDto command, CancellationToken cancellationToken)
        {
            return await _createSalesOrder.HandleAsync(command, cancellationToken);
        }

        public async Task<SalesOrderDto> UpdateSalesOrder(int id, UpdateSalesOrderDto command, CancellationToken cancellationToken)
        {
            return await _updateSalesOrder.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteSalesOrder(int id, CancellationToken cancellationToken)
        {
            return await _deleteSalesOrder.HandleAsync(id, cancellationToken);
        }
    }
}
