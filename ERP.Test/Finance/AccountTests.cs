using ERP.Domain.Entities.Finance;
using Xunit;

namespace ERP.Test.Finance
{
    public class AccountTests
    {
        [Fact]
        public void Account_Constructor_ShouldInitializeFinancialTransactionsCollection()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            // Assert
            Assert.NotNull(account.FinancialTransactions);
            Assert.Empty(account.FinancialTransactions);
        }

        [Fact]
        public void Account_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var accountName = "Cash Account";
            var accountNumber = "ACC001";
            var accountType = "Asset";

            // Act
            var account = new Account
            {
                Id = id,
                AccountName = accountName,
                AccountNumber = accountNumber,
                AccountType = accountType
            };

            // Assert
            Assert.Equal(id, account.Id);
            Assert.Equal(accountName, account.AccountName);
            Assert.Equal(accountNumber, account.AccountNumber);
            Assert.Equal(accountType, account.AccountType);
        }

        [Fact]
        public void Account_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset",
                Description = null,
                CreatedAt = null,
                UpdatedAt = null
            };

            // Assert
            Assert.Null(account.Description);
            Assert.Null(account.CreatedAt);
            Assert.Null(account.UpdatedAt);
        }

        [Fact]
        public void Account_OptionalProperties_CanBeSet()
        {
            // Arrange
            var description = "Main cash account for daily operations";
            var createdAt = DateTime.UtcNow;
            var updatedAt = DateTime.UtcNow.AddHours(1);

            // Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Cash Account",
                AccountNumber = "ACC001",
                AccountType = "Asset",
                Description = description,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal(description, account.Description);
            Assert.Equal(createdAt, account.CreatedAt);
            Assert.Equal(updatedAt, account.UpdatedAt);
        }

        [Fact]
        public void Account_Balance_DefaultValue_ShouldBeZero()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            // Assert
            Assert.Equal(0, account.Balance);
        }

        [Theory]
        [InlineData(1000.50)]
        [InlineData(-500.25)]
        [InlineData(0)]
        [InlineData(999999.99)]
        public void Account_Balance_AcceptsValidDecimalValues(decimal balance)
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset",
                Balance = balance
            };

            // Assert
            Assert.Equal(balance, account.Balance);
        }

        [Fact]
        public void Account_IsActive_DefaultValue_ShouldBeTrue()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            // Assert
            Assert.True(account.IsActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Account_IsActive_CanBeSetToAnyBooleanValue(bool isActive)
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset",
                IsActive = isActive
            };

            // Assert
            Assert.Equal(isActive, account.IsActive);
        }

        [Theory]
        [InlineData("Asset")]
        [InlineData("Liability")]
        [InlineData("Equity")]
        [InlineData("Revenue")]
        [InlineData("Expense")]
        public void Account_AccountType_AcceptsValidAccountTypes(string accountType)
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = accountType
            };

            // Assert
            Assert.Equal(accountType, account.AccountType);
        }

        [Theory]
        [InlineData("ACC001")]
        [InlineData("CASH-001")]
        [InlineData("1000")]
        [InlineData("AR_001")]
        public void Account_AccountNumber_AcceptsValidFormats(string accountNumber)
        {
            // Arrange & Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = accountNumber,
                AccountType = "Asset"
            };

            // Assert
            Assert.Equal(accountNumber, account.AccountNumber);
        }

        [Fact]
        public void Account_FinancialTransactions_NavigationProperty_CanAddTransactions()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                AccountName = "Cash Account",
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            var transaction1 = new FinancialTransaction
            {
                Id = 1,
                AccountId = account.Id,
                Amount = 1000.00m,
                TransactionType = "Credit",
                Description = "Initial deposit",
                TransactionDate = DateTime.Now
            };

            var transaction2 = new FinancialTransaction
            {
                Id = 2,
                AccountId = account.Id,
                Amount = -500.00m,
                TransactionType = "Debit",
                Description = "Withdrawal",
                TransactionDate = DateTime.UtcNow
            };

            // Act
            account.FinancialTransactions.Add(transaction1);
            account.FinancialTransactions.Add(transaction2);

            // Assert
            Assert.Equal(2, account.FinancialTransactions.Count);
            Assert.Contains(transaction1, account.FinancialTransactions);
            Assert.Contains(transaction2, account.FinancialTransactions);
        }

        [Fact]
        public void Account_AccountName_AcceptsLongNames()
        {
            // Arrange
            var longAccountName = new string('A', 255); // Máximo permitido según configuración

            // Act
            var account = new Account
            {
                Id = 1,
                AccountName = longAccountName,
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            // Assert
            Assert.Equal(longAccountName, account.AccountName);
        }

        [Fact]
        public void Account_Description_AcceptsLongText()
        {
            // Arrange
            var longDescription = new string('D', 1000); // Descripción larga

            // Act
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC001",
                AccountType = "Asset",
                Description = longDescription
            };

            // Assert
            Assert.Equal(longDescription, account.Description);
        }
    }
}