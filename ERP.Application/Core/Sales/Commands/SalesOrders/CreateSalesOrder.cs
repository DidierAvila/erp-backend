using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Sales.Commands.SalesOrders
{
    public class CreateSalesOrder
    {
        private readonly IRepositoryBase<SalesOrder> _salesOrderRepository;
        private readonly IRepositoryBase<User> _customerRepository;
        private readonly IMapper _mapper;

        public CreateSalesOrder(IRepositoryBase<SalesOrder> salesOrderRepository, IRepositoryBase<User> customerRepository, IMapper mapper)
        {
            _salesOrderRepository = salesOrderRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<SalesOrderDto> HandleAsync(CreateSalesOrderDto createSalesOrderDto, CancellationToken cancellationToken)
        {
            // Validations
            if (createSalesOrderDto.CustomerId == Guid.Empty)
                throw new ArgumentException("Customer ID is required");

            if (string.IsNullOrWhiteSpace(createSalesOrderDto.Status))
                throw new ArgumentException("Status is required");

            if (createSalesOrderDto.Items == null || !createSalesOrderDto.Items.Any())
                throw new ArgumentException("At least one item is required");

            // Check if customer exists
            var customer = await _customerRepository.Find(x => x.Id == createSalesOrderDto.CustomerId, cancellationToken);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {createSalesOrderDto.CustomerId} not found");

            // Calculate total amount from items
            decimal totalAmount = createSalesOrderDto.Items.Sum(item => item.Quantity * item.UnitPrice);

            // Create sales order
            var salesOrder = new SalesOrder
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                CustomerId = createSalesOrderDto.CustomerId,
                Status = createSalesOrderDto.Status,
                TotalAmount = totalAmount,
                SalesOrderItems = createSalesOrderDto.Items.Select(item => new SalesOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            var createdSalesOrder = await _salesOrderRepository.Create(salesOrder, cancellationToken);

            // Map to DTO
            return _mapper.Map<SalesOrderDto>(createdSalesOrder);
        }
    }
}
