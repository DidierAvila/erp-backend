using ERP.Domain.Entities.Purchases;
using Xunit;

namespace ERP.Test.Purchases
{
    public class SupplierTests
    {
        [Fact]
        public void Supplier_Constructor_ShouldInitializePurchaseOrdersCollection()
        {
            // Arrange & Act
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = "Test Supplier",
                ContactEmail = "test@supplier.com"
            };

            // Assert
            Assert.NotNull(supplier.PurchaseOrders);
            Assert.Empty(supplier.PurchaseOrders);
        }

        [Fact]
        public void Supplier_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var supplierName = "ABC Electronics Ltd.";
            var contactEmail = "contact@abcelectronics.com";

            // Act
            var supplier = new Supplier
            {
                Id = id,
                SupplierName = supplierName,
                ContactEmail = contactEmail
            };

            // Assert
            Assert.Equal(id, supplier.Id);
            Assert.Equal(supplierName, supplier.SupplierName);
            Assert.Equal(contactEmail, supplier.ContactEmail);
        }

        [Theory]
        [InlineData("contact@supplier.com")]
        [InlineData("info@abcelectronics.co.uk")]
        [InlineData("sales.department@company-name.org")]
        [InlineData("user+tag@domain.com")]
        public void Supplier_ContactEmail_AcceptsValidEmailFormats(string email)
        {
            // Arrange & Act
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = "Test Supplier",
                ContactEmail = email
            };

            // Assert
            Assert.Equal(email, supplier.ContactEmail);
        }

        [Theory]
        [InlineData("+1-555-123-4567")]
        [InlineData("(555) 123-4567")]
        [InlineData("555.123.4567")]
        [InlineData("+44 20 7946 0958")]
        [InlineData("5551234567")]
        public void Supplier_ContactPhone_AcceptsValidPhoneFormats(string phone)
        {
            // Arrange & Act
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = "Test Supplier",
                ContactEmail = "test@supplier.com",
                ContactPhone = phone
            };

            // Assert
            Assert.Equal(phone, supplier.ContactPhone);
        }

        [Fact]
        public void Supplier_SupplierName_AcceptsLongNames()
        {
            // Arrange
            var longSupplierName = new string('S', 255); // Máximo permitido según configuración

            // Act
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = longSupplierName,
                ContactEmail = "test@supplier.com"
            };

            // Assert
            Assert.Equal(longSupplierName, supplier.SupplierName);
        }

        [Fact]
        public void Supplier_Address_AcceptsLongAddresses()
        {
            // Arrange
            var longAddress = new string('A', 500); // Dirección larga

            // Act
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = "Test Supplier",
                ContactEmail = "test@supplier.com",
                Address = longAddress
            };

            // Assert
            Assert.Equal(longAddress, supplier.Address);
        }

        [Fact]
        public void Supplier_PurchaseOrders_NavigationProperty_CanAddOrders()
        {
            // Arrange
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = "ABC Electronics Ltd.",
                ContactEmail = "contact@abcelectronics.com"
            };

            var purchaseOrder1 = new PurchaseOrder
            {
                Id = 1,
                SupplierId = supplier.Id,
                OrderDate = DateTime.UtcNow,
                TotalAmount = 1500.00m,
                Status = "Pending"
            };

            var purchaseOrder2 = new PurchaseOrder
            {
                Id = 2,
                SupplierId = supplier.Id,
                OrderDate = DateTime.UtcNow.AddDays(1),
                TotalAmount = 2500.00m,
                Status = "Approved"
            };

            // Act
            supplier.PurchaseOrders.Add(purchaseOrder1);
            supplier.PurchaseOrders.Add(purchaseOrder2);

            // Assert
            Assert.Equal(2, supplier.PurchaseOrders.Count);
            Assert.Contains(purchaseOrder1, supplier.PurchaseOrders);
            Assert.Contains(purchaseOrder2, supplier.PurchaseOrders);
        }

        [Fact]
        public void Supplier_SupplierName_AcceptsSpecialCharacters()
        {
            // Arrange
            var supplierNameWithSpecialChars = "ABC Electronics Ltd. & Co. - Suppliers Inc. (USA)";

            // Act
            var supplier = new Supplier
            {
                Id = 1,
                SupplierName = supplierNameWithSpecialChars,
                ContactEmail = "contact@abcelectronics.com"
            };

            // Assert
            Assert.Equal(supplierNameWithSpecialChars, supplier.SupplierName);
        }
    }
}