using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Queries.SalesOrders
{
    public class GetAllSalesOrders
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;
        private readonly IMapper _mapper;

        public GetAllSalesOrders(IRepositoryBase<SalesOrder> salesOrderRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesOrderDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var salesOrders = await _salesOrderRepository.GetAll(cancellationToken);

            // Map collection of Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<SalesOrderDto>>(salesOrders);
        }
    }
}
