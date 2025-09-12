using ERP.Domain.Entities.Inventory;

namespace ERP.Test.Inventory
{
    public class StockMovementTests
    {
        [Fact]
        public void StockMovement_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var productId = 100;
            var movementType = "IN";
            var quantity = 50;
            var movementDate = DateTime.UtcNow;

            // Act
            var stockMovement = new StockMovement
            {
                Id = id,
                ProductId = productId,
                MovementType = movementType,
                Quantity = quantity,
                MovementDate = movementDate
            };

            // Assert
            Assert.Equal(id, stockMovement.Id);
            Assert.Equal(productId, stockMovement.ProductId);
            Assert.Equal(movementType, stockMovement.MovementType);
            Assert.Equal(quantity, stockMovement.Quantity);
            Assert.Equal(movementDate, stockMovement.MovementDate);
        }

        [Fact]
        public void StockMovement_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "IN",
                Quantity = 50,
                MovementDate = DateTime.UtcNow,
                FromLocationId = null,
                ToLocationId = null
            };

            // Assert
            Assert.Null(stockMovement.FromLocationId);
            Assert.Null(stockMovement.ToLocationId);
        }

        [Fact]
        public void StockMovement_OptionalProperties_CanBeSet()
        {
            // Arrange
            var fromLocationId = 1;
            var toLocationId = 2;

            // Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "TRANSFER",
                Quantity = 25,
                MovementDate = DateTime.UtcNow,
                FromLocationId = fromLocationId,
                ToLocationId = toLocationId
            };

            // Assert
            Assert.Equal(fromLocationId, stockMovement.FromLocationId);
            Assert.Equal(toLocationId, stockMovement.ToLocationId);
        }

        [Theory]
        [InlineData("IN")]
        [InlineData("OUT")]
        [InlineData("TRANSFER")]
        [InlineData("ADJUSTMENT")]
        [InlineData("RETURN")]
        public void StockMovement_MovementType_AcceptsValidTypes(string movementType)
        {
            // Arrange & Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = movementType,
                Quantity = 10,
                MovementDate = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(movementType, stockMovement.MovementType);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(9999)]
        public void StockMovement_Quantity_AcceptsPositiveValues(int quantity)
        {
            // Arrange & Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "IN",
                Quantity = quantity,
                MovementDate = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(quantity, stockMovement.Quantity);
        }

        [Fact]
        public void StockMovement_MovementDate_AcceptsPastDates()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-30);

            // Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "IN",
                Quantity = 50,
                MovementDate = pastDate
            };

            // Assert
            Assert.Equal(pastDate, stockMovement.MovementDate);
        }

        [Fact]
        public void StockMovement_MovementDate_AcceptsFutureDates()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(7);

            // Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "IN",
                Quantity = 50,
                MovementDate = futureDate
            };

            // Assert
            Assert.Equal(futureDate, stockMovement.MovementDate);
        }

        [Fact]
        public void StockMovement_Product_NavigationProperty_CanBeSet()
        {
            // Arrange
            var product = new Product
            {
                Id = 100,
                ProductName = "Laptop Computer",
                Sku = "LAP001"
            };

            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = product.Id,
                MovementType = "IN",
                Quantity = 50,
                MovementDate = DateTime.UtcNow
            };

            // Act
            stockMovement.Product = product;

            // Assert
            Assert.NotNull(stockMovement.Product);
            Assert.Equal(product.Id, stockMovement.Product.Id);
            Assert.Equal(product.ProductName, stockMovement.Product.ProductName);
        }

        [Fact]
        public void StockMovement_FromLocation_NavigationProperty_CanBeSet()
        {
            // Arrange
            var fromLocation = new InventoryLocation
            {
                Id = 1,
                LocationName = "Warehouse A"
            };

            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "TRANSFER",
                Quantity = 25,
                MovementDate = DateTime.UtcNow,
                FromLocationId = fromLocation.Id
            };

            // Act
            stockMovement.FromLocation = fromLocation;

            // Assert
            Assert.NotNull(stockMovement.FromLocation);
            Assert.Equal(fromLocation.Id, stockMovement.FromLocation.Id);
            Assert.Equal(fromLocation.LocationName, stockMovement.FromLocation.LocationName);
        }

        [Fact]
        public void StockMovement_ToLocation_NavigationProperty_CanBeSet()
        {
            // Arrange
            var toLocation = new InventoryLocation
            {
                Id = 2,
                LocationName = "Warehouse B"
            };

            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "TRANSFER",
                Quantity = 25,
                MovementDate = DateTime.UtcNow,
                ToLocationId = toLocation.Id
            };

            // Act
            stockMovement.ToLocation = toLocation;

            // Assert
            Assert.NotNull(stockMovement.ToLocation);
            Assert.Equal(toLocation.Id, stockMovement.ToLocation.Id);
            Assert.Equal(toLocation.LocationName, stockMovement.ToLocation.LocationName);
        }

        [Fact]
        public void StockMovement_LocationIds_ShouldMatchNavigationProperties()
        {
            // Arrange
            var fromLocationId = 1;
            var toLocationId = 2;
            
            var fromLocation = new InventoryLocation
            {
                Id = fromLocationId,
                LocationName = "Warehouse A"
            };

            var toLocation = new InventoryLocation
            {
                Id = toLocationId,
                LocationName = "Warehouse B"
            };

            // Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "TRANSFER",
                Quantity = 25,
                MovementDate = DateTime.UtcNow,
                FromLocationId = fromLocationId,
                ToLocationId = toLocationId,
                FromLocation = fromLocation,
                ToLocation = toLocation
            };

            // Assert
            Assert.Equal(stockMovement.FromLocationId, stockMovement.FromLocation.Id);
            Assert.Equal(stockMovement.ToLocationId, stockMovement.ToLocation.Id);
        }

        [Fact]
        public void StockMovement_TransferMovement_ShouldHaveBothLocations()
        {
            // Arrange
            var fromLocationId = 1;
            var toLocationId = 2;

            // Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = "TRANSFER",
                Quantity = 25,
                MovementDate = DateTime.UtcNow,
                FromLocationId = fromLocationId,
                ToLocationId = toLocationId
            };

            // Assert
            Assert.Equal("TRANSFER", stockMovement.MovementType);
            Assert.Equal(fromLocationId, stockMovement.FromLocationId);
            Assert.Equal(toLocationId, stockMovement.ToLocationId);
            Assert.NotEqual(stockMovement.FromLocationId, stockMovement.ToLocationId);
        }

        [Theory]
        [InlineData("IN", null, 1)] // Entrada: solo ToLocation
        [InlineData("OUT", 1, null)] // Salida: solo FromLocation
        public void StockMovement_InOutMovements_ShouldHaveCorrectLocations(string movementType, int? fromLocationId, int? toLocationId)
        {
            // Arrange & Act
            var stockMovement = new StockMovement
            {
                Id = 1,
                ProductId = 100,
                MovementType = movementType,
                Quantity = 50,
                MovementDate = DateTime.UtcNow,
                FromLocationId = fromLocationId,
                ToLocationId = toLocationId
            };

            // Assert
            Assert.Equal(movementType, stockMovement.MovementType);
            Assert.Equal(fromLocationId, stockMovement.FromLocationId);
            Assert.Equal(toLocationId, stockMovement.ToLocationId);
        }
    }
}