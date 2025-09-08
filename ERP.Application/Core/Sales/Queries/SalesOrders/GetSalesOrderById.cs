using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Queries.SalesOrders
{
    public class GetSalesOrderById
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;
        private readonly IMapper _mapper;

        public GetSalesOrderById(IRepositoryBase<SalesOrder> salesOrderRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _mapper = mapper;
        }

        public async Task<SalesOrderDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var salesOrder = await _salesOrderRepository.GetByID(id, cancellationToken);
            
            return salesOrder != null ? _mapper.Map<SalesOrderDto>(salesOrder) : null;
        }
    }
}
