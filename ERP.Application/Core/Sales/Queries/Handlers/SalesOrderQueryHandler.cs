using ERP.Application.Core.Sales.Queries.SalesOrders;
using ERP.Domain.DTOs.Sales;

namespace ERP.Application.Core.Sales.Queries.Handlers
{
    public class SalesOrderQueryHandler : ISalesOrderQueryHandler
    {
        private readonly GetSalesOrderById _getSalesOrderById;
        private readonly GetAllSalesOrders _getAllSalesOrders;
        private readonly GetSalesOrdersByCustomer _getSalesOrdersByCustomer;
        private readonly GetSalesOrdersByStatus _getSalesOrdersByStatus;

        public SalesOrderQueryHandler(
            GetSalesOrderById getSalesOrderById, 
            GetAllSalesOrders getAllSalesOrders, 
            GetSalesOrdersByCustomer getSalesOrdersByCustomer,
            GetSalesOrdersByStatus getSalesOrdersByStatus)
        {
            _getSalesOrderById = getSalesOrderById;
            _getAllSalesOrders = getAllSalesOrders;
            _getSalesOrdersByCustomer = getSalesOrdersByCustomer;
            _getSalesOrdersByStatus = getSalesOrdersByStatus;
        }

        public async Task<SalesOrderDto?> GetSalesOrderById(int id, CancellationToken cancellationToken)
        {
            return await _getSalesOrderById.HandleAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<SalesOrderDto>> GetAllSalesOrders(CancellationToken cancellationToken)
        {
            return await _getAllSalesOrders.HandleAsync(cancellationToken);
        }

        public async Task<IEnumerable<SalesOrderDto>> GetSalesOrdersByCustomer(int customerId, CancellationToken cancellationToken)
        {
            return await _getSalesOrdersByCustomer.HandleAsync(customerId, cancellationToken);
        }

        public async Task<IEnumerable<SalesOrderDto>> GetSalesOrdersByStatus(string status, CancellationToken cancellationToken)
        {
            return await _getSalesOrdersByStatus.HandleAsync(status, cancellationToken);
        }
    }
}
