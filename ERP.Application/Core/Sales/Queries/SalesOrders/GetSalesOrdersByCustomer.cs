using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Queries.SalesOrders
{
    public class GetSalesOrdersByCustomer
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;
        private readonly IMapper _mapper;

        public GetSalesOrdersByCustomer(IRepositoryBase<SalesOrder> salesOrderRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesOrderDto>> HandleAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var salesOrders = await _salesOrderRepository.Finds(
                x => x.CustomerId == customerId, 
                cancellationToken);

            return _mapper.Map<IEnumerable<SalesOrderDto>>(salesOrders ?? new List<SalesOrder>());
        }
    }
}
