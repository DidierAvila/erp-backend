using ERP.Domain.DTOs.Sales;

namespace ERP.Application.Core.Sales.Commands.Handlers
{
    public interface ISalesOrderCommandHandler
    {
        Task<SalesOrderDto> CreateSalesOrder(CreateSalesOrderDto command, CancellationToken cancellationToken);
        Task<SalesOrderDto> UpdateSalesOrder(int id, UpdateSalesOrderDto command, CancellationToken cancellationToken);
        Task<bool> DeleteSalesOrder(int id, CancellationToken cancellationToken);
    }
}
