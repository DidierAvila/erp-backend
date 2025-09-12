using ERP.Domain.Entities.Sales;

namespace ERP.Test.Sales
{
    public class SalesOrderItemTests
    {
        [Fact]
        public void SalesOrderItem_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var salesOrderId = 100;
            var productId = 200;
            var quantity = 5;
            var unitPrice = 250.75m;

            // Act
            var salesOrderItem = new SalesOrderItem
            {
                Id = id,
                SalesOrderId = salesOrderId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = unitPrice
            };

            // Assert
            Assert.Equal(id, salesOrderItem.Id);
            Assert.Equal(salesOrderId, salesOrderItem.SalesOrderId);
            Assert.Equal(productId, salesOrderItem.ProductId);
            Assert.Equal(quantity, salesOrderItem.Quantity);
            Assert.Equal(unitPrice, salesOrderItem.UnitPrice);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void SalesOrderItem_Quantity_AcceptsValidPositiveValues(int quantity)
        {
            // Arrange & Act
            var salesOrderItem = new SalesOrderItem
            {
                Id = 1,
                SalesOrderId = 100,
                ProductId = 200,
                Quantity = quantity,
                UnitPrice = 100.00m
            };

            // Assert
            Assert.Equal(quantity, salesOrderItem.Quantity);
        }

        [Theory]
        [InlineData(0.01)]
        [InlineData(10.50)]
        [InlineData(100.75)]
        [InlineData(1500.99)]
        [InlineData(9999.99)]
        public void SalesOrderItem_UnitPrice_AcceptsValidDecimalValues(decimal unitPrice)
        {
            // Arrange & Act
            var salesOrderItem = new SalesOrderItem
            {
                Id = 1,
                SalesOrderId = 100,
                ProductId = 200,
                Quantity = 2,
                UnitPrice = unitPrice
            };

            // Assert
            Assert.Equal(unitPrice, salesOrderItem.UnitPrice);
        }
    }
}