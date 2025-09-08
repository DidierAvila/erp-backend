using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Queries.SalesOrders
{
    public class GetSalesOrdersByStatus
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;
        private readonly IMapper _mapper;

        public GetSalesOrdersByStatus(IRepositoryBase<SalesOrder> salesOrderRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesOrderDto>> HandleAsync(string status, CancellationToken cancellationToken)
        {
            var salesOrders = await _salesOrderRepository.Finds(
                x => x.Status.ToLower() == status.ToLower(), 
                cancellationToken);

            return _mapper.Map<IEnumerable<SalesOrderDto>>(salesOrders ?? new List<SalesOrder>());
        }
    }
}
