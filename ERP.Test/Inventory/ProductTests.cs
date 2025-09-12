using ERP.Domain.Entities.Inventory;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Entities.Sales;
using Xunit;

namespace ERP.Test.Inventory
{
    public class ProductTests
    {
        [Fact]
        public void Product_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = "SKU001"
            };

            // Assert
            Assert.NotNull(product.PurchaseOrderItems);
            Assert.Empty(product.PurchaseOrderItems);
            Assert.NotNull(product.SalesOrderItems);
            Assert.Empty(product.SalesOrderItems);
            Assert.NotNull(product.StockMovements);
            Assert.Empty(product.StockMovements);
        }

        [Fact]
        public void Product_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var productName = "Laptop Computer";
            var sku = "LAP001";

            // Act
            var product = new Product
            {
                Id = id,
                ProductName = productName,
                Sku = sku
            };

            // Assert
            Assert.Equal(id, product.Id);
            Assert.Equal(productName, product.ProductName);
            Assert.Equal(sku, product.Sku);
        }

        [Fact]
        public void Product_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = "SKU001",
                Description = null
            };

            // Assert
            Assert.Null(product.Description);
        }

        [Fact]
        public void Product_OptionalProperties_CanBeSet()
        {
            // Arrange
            var description = "High-performance laptop for business use";
            var unitOfMeasure = "Each";

            // Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop Computer",
                Sku = "LAP001",
                Description = description
            };

            // Assert
            Assert.Equal(description, product.Description);
            Assert.Equal(unitOfMeasure, product.UnitOfMeasure);
        }

        [Fact]
        public void Product_CurrentStock_DefaultValue_ShouldBeZero()
        {
            // Arrange & Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = "SKU001"
            };

            // Assert
            Assert.Equal(0, product.CurrentStock);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(9999)]
        public void Product_CurrentStock_AcceptsValidIntegerValues(int stock)
        {
            // Arrange & Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = "SKU001",
                CurrentStock = stock
            };

            // Assert
            Assert.Equal(stock, product.CurrentStock);
        }

        [Theory]
        [InlineData("SKU001")]
        [InlineData("PROD-2024-001")]
        [InlineData("LAP_001")]
        [InlineData("12345")]
        [InlineData("ABC-XYZ-123")]
        public void Product_Sku_AcceptsValidFormats(string sku)
        {
            // Arrange & Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = sku
            };

            // Assert
            Assert.Equal(sku, product.Sku);
        }

        [Fact]
        public void Product_ProductName_AcceptsLongNames()
        {
            // Arrange
            var longProductName = new string('P', 255); // Máximo permitido según configuración

            // Act
            var product = new Product
            {
                Id = 1,
                ProductName = longProductName,
                Sku = "SKU001"
            };

            // Assert
            Assert.Equal(longProductName, product.ProductName);
        }

        [Fact]
        public void Product_Description_AcceptsLongText()
        {
            // Arrange
            var longDescription = new string('D', 1000); // Descripción larga

            // Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = "SKU001",
                Description = longDescription
            };

            // Assert
            Assert.Equal(longDescription, product.Description);
        }

        [Theory]
        [InlineData("Each")]
        [InlineData("Kg")]
        [InlineData("Liter")]
        [InlineData("Box")]
        [InlineData("Meter")]
        public void Product_UnitOfMeasure_AcceptsValidUnits(string unitOfMeasure)
        {
            // Arrange & Act
            var product = new Product
            {
                Id = 1,
                ProductName = "Test Product",
                Sku = "SKU001",
                UnitOfMeasure = unitOfMeasure
            };

            // Assert
            Assert.Equal(unitOfMeasure, product.UnitOfMeasure);
        }

        [Fact]
        public void Product_PurchaseOrderItems_NavigationProperty_CanAddItems()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop Computer",
                Sku = "LAP001"
            };

            var purchaseOrderItem1 = new PurchaseOrderItem
            {
                Id = 1,
                ProductId = product.Id,
                Quantity = 10,
                UnitPrice = 1000.00m
            };

            var purchaseOrderItem2 = new PurchaseOrderItem
            {
                Id = 2,
                ProductId = product.Id,
                Quantity = 5,
                UnitPrice = 1050.00m
            };

            // Act
            product.PurchaseOrderItems.Add(purchaseOrderItem1);
            product.PurchaseOrderItems.Add(purchaseOrderItem2);

            // Assert
            Assert.Equal(2, product.PurchaseOrderItems.Count);
            Assert.Contains(purchaseOrderItem1, product.PurchaseOrderItems);
            Assert.Contains(purchaseOrderItem2, product.PurchaseOrderItems);
        }

        [Fact]
        public void Product_SalesOrderItems_NavigationProperty_CanAddItems()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop Computer",
                Sku = "LAP001"
            };

            var salesOrderItem1 = new SalesOrderItem
            {
                Id = 1,
                ProductId = product.Id,
                Quantity = 2,
                UnitPrice = 1200.00m
            };

            var salesOrderItem2 = new SalesOrderItem
            {
                Id = 2,
                ProductId = product.Id,
                Quantity = 1,
                UnitPrice = 1250.00m
            };

            // Act
            product.SalesOrderItems.Add(salesOrderItem1);
            product.SalesOrderItems.Add(salesOrderItem2);

            // Assert
            Assert.Equal(2, product.SalesOrderItems.Count);
            Assert.Contains(salesOrderItem1, product.SalesOrderItems);
            Assert.Contains(salesOrderItem2, product.SalesOrderItems);
        }

        [Fact]
        public void Product_StockMovements_NavigationProperty_CanAddMovements()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop Computer",
                Sku = "LAP001"
            };

            var stockMovement1 = new StockMovement
            {
                Id = 1,
                ProductId = product.Id,
                MovementType = "IN",
                Quantity = 50,
                MovementDate = DateTime.UtcNow
            };

            var stockMovement2 = new StockMovement
            {
                Id = 2,
                ProductId = product.Id,
                MovementType = "OUT",
                Quantity = 10,
                MovementDate = DateTime.UtcNow
            };

            // Act
            product.StockMovements.Add(stockMovement1);
            product.StockMovements.Add(stockMovement2);

            // Assert
            Assert.Equal(2, product.StockMovements.Count);
            Assert.Contains(stockMovement1, product.StockMovements);
            Assert.Contains(stockMovement2, product.StockMovements);
        }

        [Fact]
        public void Product_ProductName_AcceptsSpecialCharacters()
        {
            // Arrange
            var productNameWithSpecialChars = "Laptop Dell XPS 13\" - Model 2024 (Silver) - 16GB RAM";

            // Act
            var product = new Product
            {
                Id = 1,
                ProductName = productNameWithSpecialChars,
                Sku = "LAP001"
            };

            // Assert
            Assert.Equal(productNameWithSpecialChars, product.ProductName);
        }
    }
}