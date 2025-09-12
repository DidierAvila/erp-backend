using ERP.Domain.Entities.Auth;
using ERP.Domain.Entities.Purchases;

namespace ERP.Test.Purchases
{
    public class PurchaseOrderTests
    {
        [Fact]
        public void PurchaseOrder_Constructor_ShouldInitializePurchaseOrderItemsCollection()
        {
            // Arrange & Act
            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = 1,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000.00m,
                Status = "Pending"
            };

            // Assert
            Assert.NotNull(purchaseOrder.PurchaseOrderItems);
            Assert.Empty(purchaseOrder.PurchaseOrderItems);
        }

        [Fact]
        public void PurchaseOrder_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var supplierId = 100;
            var orderDate = DateTime.UtcNow;
            var totalAmount = 2500.75m;
            var status = "Approved";

            // Act
            var purchaseOrder = new PurchaseOrder
            {
                Id = id,
                SupplierId = supplierId,
                OrderDate = orderDate,
                TotalAmount = totalAmount,
                Status = status
            };

            // Assert
            Assert.Equal(id, purchaseOrder.Id);
            Assert.Equal(supplierId, purchaseOrder.SupplierId);
            Assert.Equal(orderDate, purchaseOrder.OrderDate);
            Assert.Equal(totalAmount, purchaseOrder.TotalAmount);
            Assert.Equal(status, purchaseOrder.Status);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100.50)]
        [InlineData(2500.75)]
        [InlineData(99999.99)]
        public void PurchaseOrder_TotalAmount_AcceptsValidDecimalValues(decimal totalAmount)
        {
            // Arrange & Act
            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = 100,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Pending"
            };

            // Assert
            Assert.Equal(totalAmount, purchaseOrder.TotalAmount);
        }

        [Theory]
        [InlineData("Pending")]
        [InlineData("Approved")]
        [InlineData("Rejected")]
        [InlineData("Sent")]
        [InlineData("Received")]
        [InlineData("Cancelled")]
        [InlineData("Completed")]
        public void PurchaseOrder_Status_AcceptsValidStatuses(string status)
        {
            // Arrange & Act
            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = 100,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000.00m,
                Status = status
            };

            // Assert
            Assert.Equal(status, purchaseOrder.Status);
        }

        [Fact]
        public void PurchaseOrder_OrderDate_AcceptsPastDates()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-30);

            // Act
            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = 100,
                OrderDate = pastDate,
                TotalAmount = 1000.00m,
                Status = "Completed"
            };

            // Assert
            Assert.Equal(pastDate, purchaseOrder.OrderDate);
        }

        [Fact]
        public void PurchaseOrder_OrderDate_AcceptsFutureDates()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(7);

            // Act
            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = 100,
                OrderDate = futureDate,
                TotalAmount = 1000.00m,
                Status = "Pending"
            };

            // Assert
            Assert.Equal(futureDate, purchaseOrder.OrderDate);
        }

        [Fact]
        public void PurchaseOrder_Supplier_NavigationProperty_CanBeSet()
        {
            // Arrange
            var supplier = new Supplier
            {
                Id = 100,
                SupplierName = "ABC Electronics Ltd.",
                ContactEmail = "contact@abcelectronics.com"
            };

            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = supplier.Id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1000.00m,
                Status = "Pending"
            };

            // Act
            purchaseOrder.Supplier = supplier;

            // Assert
            Assert.NotNull(purchaseOrder.Supplier);
            Assert.Equal(supplier.Id, purchaseOrder.Supplier.Id);
            Assert.Equal(supplier.SupplierName, purchaseOrder.Supplier.SupplierName);
        }

        [Fact]
        public void PurchaseOrder_PurchaseOrderItems_NavigationProperty_CanAddItems()
        {
            // Arrange
            var purchaseOrder = new PurchaseOrder
            {
                Id = 1,
                SupplierId = 100,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 3000.00m,
                Status = "Pending"
            };

            var item1 = new PurchaseOrderItem
            {
                Id = 1,
                PurchaseOrderId = purchaseOrder.Id,
                ProductId = 1,
                Quantity = 10,
                UnitPrice = 150.00m
            };

            var item2 = new PurchaseOrderItem
            {
                Id = 2,
                PurchaseOrderId = purchaseOrder.Id,
                ProductId = 2,
                Quantity = 5,
                UnitPrice = 300.00m
            };

            // Act
            purchaseOrder.PurchaseOrderItems.Add(item1);
            purchaseOrder.PurchaseOrderItems.Add(item2);

            // Assert
            Assert.Equal(2, purchaseOrder.PurchaseOrderItems.Count);
            Assert.Contains(item1, purchaseOrder.PurchaseOrderItems);
            Assert.Contains(item2, purchaseOrder.PurchaseOrderItems);
        }
    }
}