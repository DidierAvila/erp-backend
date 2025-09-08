using ERP.Domain.DTOs.Sales;

namespace ERP.Application.Core.Sales.Queries.Handlers
{
    public interface ISalesOrderQueryHandler
    {
        Task<SalesOrderDto?> GetSalesOrderById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<SalesOrderDto>> GetAllSalesOrders(CancellationToken cancellationToken);
        Task<IEnumerable<SalesOrderDto>> GetSalesOrdersByCustomer(int customerId, CancellationToken cancellationToken);
        Task<IEnumerable<SalesOrderDto>> GetSalesOrdersByStatus(string status, CancellationToken cancellationToken);
    }
}
