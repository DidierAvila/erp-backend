using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Commands.SalesOrders
{
    public class UpdateSalesOrder
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;
        private readonly IMapper _mapper;

        public UpdateSalesOrder(IRepositoryBase<SalesOrder> salesOrderRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _mapper = mapper;
        }

        public async Task<SalesOrderDto> HandleAsync(int id, UpdateSalesOrderDto updateSalesOrderDto, CancellationToken cancellationToken)
        {
            // Validations
            if (updateSalesOrderDto.CustomerId <= 0)
                throw new ArgumentException("Customer ID is required");

            if (string.IsNullOrWhiteSpace(updateSalesOrderDto.Status))
                throw new ArgumentException("Status is required");

            // Get existing sales order
            var existingSalesOrder = await _salesOrderRepository.GetByID(id, cancellationToken);
            if (existingSalesOrder == null)
                throw new KeyNotFoundException($"Sales order with ID {id} not found");

            // Update basic fields
            existingSalesOrder.CustomerId = updateSalesOrderDto.CustomerId;
            existingSalesOrder.Status = updateSalesOrderDto.Status;

            // Update total amount if items provided
            if (updateSalesOrderDto.Items != null && updateSalesOrderDto.Items.Any())
            {
                decimal totalAmount = updateSalesOrderDto.Items.Sum(item => item.Quantity * item.UnitPrice);
                existingSalesOrder.TotalAmount = totalAmount;
            }

            await _salesOrderRepository.Update(existingSalesOrder, cancellationToken);

            return _mapper.Map<SalesOrderDto>(existingSalesOrder);
        }
    }
}
