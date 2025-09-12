using ERP.Domain.Entities.Sales;
using ERP.Domain.Entities.Auth;
using Xunit;

namespace ERP.Test.Sales
{
    public class SalesOrderTests
    {
        [Fact]
        public void SalesOrder_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000.00m,
                Status = "Pending"
            };

            // Assert
            Assert.NotNull(salesOrder.SalesOrderItems);
            Assert.Empty(salesOrder.SalesOrderItems);
            Assert.NotNull(salesOrder.Invoices);
            Assert.Empty(salesOrder.Invoices);
        }

        [Fact]
        public void SalesOrder_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var customerId = Guid.NewGuid();
            var orderDate = DateTime.UtcNow;
            var totalAmount = 2500.75m;
            var status = "Confirmed";

            // Act
            var salesOrder = new SalesOrder
            {
                Id = id,
                CustomerId = customerId,
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                Status = status
            };

            // Assert
            Assert.Equal(id, salesOrder.Id);
            Assert.Equal(customerId, salesOrder.CustomerId);
            Assert.Equal(orderDate, salesOrder.OrderDate);
            Assert.Equal(totalAmount, salesOrder.TotalAmount);
            Assert.Equal(status, salesOrder.Status);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100.50)]
        [InlineData(2500.75)]
        [InlineData(99999.99)]
        public void SalesOrder_TotalAmount_AcceptsValidDecimalValues(decimal totalAmount)
        {
            // Arrange & Act
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Pending"
            };

            // Assert
            Assert.Equal(totalAmount, salesOrder.TotalAmount);
        }

        [Theory]
        [InlineData("Pending")]
        [InlineData("Confirmed")]
        [InlineData("Processing")]
        [InlineData("Shipped")]
        [InlineData("Delivered")]
        [InlineData("Cancelled")]
        [InlineData("Completed")]
        public void SalesOrder_Status_AcceptsValidStatuses(string status)
        {
            // Arrange & Act
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000.00m,
                Status = status
            };

            // Assert
            Assert.Equal(status, salesOrder.Status);
        }

        [Theory]
        [InlineData("SO-2024-001")]
        [InlineData("SALES-001")]
        [InlineData("ORD_2024_001")]
        [InlineData("SO/2024/001")]
        [InlineData("12345")]
        public void SalesOrder_OrderNumber_AcceptsValidFormats(string orderNumber)
        {
            // Arrange & Act
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000.00m,
                Status = "Pending",
                OrderNumber = orderNumber
            };

            // Assert
            Assert.Equal(orderNumber, salesOrder.OrderNumber);
        }

        [Fact]
        public void SalesOrder_OrderDate_AcceptsPastDates()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-30);

            // Act
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = pastDate,
                TotalAmount = 1000.00m,
                Status = "Completed"
            };

            // Assert
            Assert.Equal(pastDate, salesOrder.OrderDate);
        }

        [Fact]
        public void SalesOrder_OrderDate_AcceptsFutureDates()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(7);

            // Act
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = futureDate,
                TotalAmount = 1000.00m,
                Status = "Pending"
            };

            // Assert
            Assert.Equal(futureDate, salesOrder.OrderDate);
        }


        [Fact]
        public void SalesOrder_SalesOrderItems_NavigationProperty_CanAddItems()
        {
            // Arrange
            var salesOrder = new SalesOrder
            {
                Id = 1,
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 3000.00m,
                Status = "Pending"
            };

            var item1 = new SalesOrderItem
            {
                Id = 1,
                SalesOrderId = salesOrder.Id,
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 750.00m
            };

            var item2 = new SalesOrderItem
            {
                Id = 2,
                SalesOrderId = salesOrder.Id,
                ProductId = 2,
                Quantity = 1,
                UnitPrice = 1500.00m
            };

            // Act
            salesOrder.SalesOrderItems.Add(item1);
            salesOrder.SalesOrderItems.Add(item2);

            // Assert
            Assert.Equal(2, salesOrder.SalesOrderItems.Count);
            Assert.Contains(item1, salesOrder.SalesOrderItems);
            Assert.Contains(item2, salesOrder.SalesOrderItems);
        }
    }
}