using ERP.Domain.Entities.Finance;

namespace ERP.Test.Finance
{
    public class FinancialTransactionTests
    {
        [Fact]
        public void FinancialTransaction_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = 1;
            var accountId = 100;
            var amount = 1500.75m;
            var transactionType = "Credit";
            var description = "Payment received";
            var transactionDate = DateTime.UtcNow;

            // Act
            var transaction = new FinancialTransaction
            {
                Id = id,
                AccountId = accountId,
                Amount = amount,
                TransactionType = transactionType,
                Description = description,
                TransactionDate = transactionDate
            };

            // Assert
            Assert.Equal(id, transaction.Id);
            Assert.Equal(accountId, transaction.AccountId);
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal(transactionType, transaction.TransactionType);
            Assert.Equal(description, transaction.Description);
            Assert.Equal(transactionDate, transaction.TransactionDate);
        }

        [Theory]
        [InlineData(1000.50)]
        [InlineData(-500.25)]
        [InlineData(0)]
        [InlineData(999999.99)]
        [InlineData(-999999.99)]
        public void FinancialTransaction_Amount_AcceptsValidDecimalValues(decimal amount)
        {
            // Arrange & Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = 100,
                Amount = amount,
                TransactionType = "Credit",
                Description = "Test transaction",
                TransactionDate = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(amount, transaction.Amount);
        }

        [Theory]
        [InlineData("Credit")]
        [InlineData("Debit")]
        [InlineData("Transfer")]
        [InlineData("Adjustment")]
        public void FinancialTransaction_TransactionType_AcceptsValidTypes(string transactionType)
        {
            // Arrange & Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = 100,
                Amount = 1000.00m,
                TransactionType = transactionType,
                Description = "Test transaction",
                TransactionDate = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(transactionType, transaction.TransactionType);
        }

        [Fact]
        public void FinancialTransaction_TransactionDate_AcceptsPastDates()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.AddDays(-30);

            // Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = 100,
                Amount = 1000.00m,
                TransactionType = "Credit",
                Description = "Past transaction",
                TransactionDate = pastDate
            };

            // Assert
            Assert.Equal(pastDate, transaction.TransactionDate);
        }

        [Fact]
        public void FinancialTransaction_TransactionDate_AcceptsFutureDates()
        {
            // Arrange
            var futureDate = DateTime.UtcNow.AddDays(30);

            // Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = 100,
                Amount = 1000.00m,
                TransactionType = "Credit",
                Description = "Future transaction",
                TransactionDate = futureDate
            };

            // Assert
            Assert.Equal(futureDate, transaction.TransactionDate);
        }

        [Fact]
        public void FinancialTransaction_Description_AcceptsLongText()
        {
            // Arrange
            var longDescription = new string('D', 500); // Descripción larga

            // Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = 100,
                Amount = 1000.00m,
                TransactionType = "Credit",
                Description = longDescription,
                TransactionDate = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(longDescription, transaction.Description);
        }

        [Fact]
        public void FinancialTransaction_Account_NavigationProperty_CanBeSet()
        {
            // Arrange
            var account = new Account
            {
                Id = 100,
                AccountName = "Cash Account",
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = account.Id,
                Amount = 1000.00m,
                TransactionType = "Credit",
                Description = "Test transaction",
                TransactionDate = DateTime.UtcNow
            };

            // Act
            transaction.Account = account;

            // Assert
            Assert.NotNull(transaction.Account);
            Assert.Equal(account.Id, transaction.Account.Id);
            Assert.Equal(account.AccountName, transaction.Account.AccountName);
        }

        [Fact]
        public void FinancialTransaction_AccountId_ShouldMatchAccountNavigationProperty()
        {
            // Arrange
            var accountId = 100;
            var account = new Account
            {
                Id = accountId,
                AccountName = "Cash Account",
                AccountNumber = "ACC001",
                AccountType = "Asset"
            };

            // Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = accountId,
                Amount = 1000.00m,
                TransactionType = "Credit",
                Description = "Test transaction",
                TransactionDate = DateTime.UtcNow,
                Account = account
            };

            // Assert
            Assert.Equal(transaction.AccountId, transaction.Account.Id);
        }

        [Fact]
        public void FinancialTransaction_Description_AcceptsSpecialCharacters()
        {
            // Arrange
            var descriptionWithSpecialChars = "Payment for invoice #INV-2024/001 - $1,500.00 (USD) - Client: John & Co.";

            // Act
            var transaction = new FinancialTransaction
            {
                Id = 1,
                AccountId = 100,
                Amount = 1500.00m,
                TransactionType = "Credit",
                Description = descriptionWithSpecialChars,
                TransactionDate = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(descriptionWithSpecialChars, transaction.Description);
        }
    }
}