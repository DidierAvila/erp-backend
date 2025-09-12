using ERP.Domain.Entities.Auth;

namespace ERP.Test.Auth
{
    public class AccountTests
    {
        [Fact]
        public void Account_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = Guid.NewGuid();
            var type = "oauth";
            var provider = "google";
            var providerAccountId = "123456789";

            // Act
            var account = new Account
            {
                Id = id,
                Type = type,
                Provider = provider,
                ProviderAccountId = providerAccountId
            };

            // Assert
            Assert.Equal(id, account.Id);
            Assert.Equal(type, account.Type);
            Assert.Equal(provider, account.Provider);
            Assert.Equal(providerAccountId, account.ProviderAccountId);
        }

        [Fact]
        public void Account_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = "oauth",
                Provider = "github",
                ProviderAccountId = "987654321",
                UserId = null,
                RefreshToken = null,
                AccessToken = null,
                ExpiresAt = null,
                IdToken = null,
                Scope = null,
                SessionState = null,
                TokenType = null
            };

            // Assert
            Assert.Null(account.UserId);
            Assert.Null(account.RefreshToken);
            Assert.Null(account.AccessToken);
            Assert.Null(account.ExpiresAt);
            Assert.Null(account.IdToken);
            Assert.Null(account.Scope);
            Assert.Null(account.SessionState);
            Assert.Null(account.TokenType);
        }

        [Fact]
        public void Account_OptionalProperties_CanBeSet()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var refreshToken = "refresh_token_123";
            var accessToken = "access_token_456";
            var expiresAt = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds();
            var idToken = "id_token_789";
            var scope = "read write";
            var sessionState = "session_state_abc";
            var tokenType = "Bearer";

            // Act
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = "oauth",
                Provider = "microsoft",
                ProviderAccountId = "555666777",
                UserId = userId,
                RefreshToken = refreshToken,
                AccessToken = accessToken,
                ExpiresAt = expiresAt,
                IdToken = idToken,
                Scope = scope,
                SessionState = sessionState,
                TokenType = tokenType
            };

            // Assert
            Assert.Equal(userId, account.UserId);
            Assert.Equal(refreshToken, account.RefreshToken);
            Assert.Equal(accessToken, account.AccessToken);
            Assert.Equal(expiresAt, account.ExpiresAt);
            Assert.Equal(idToken, account.IdToken);
            Assert.Equal(scope, account.Scope);
            Assert.Equal(sessionState, account.SessionState);
            Assert.Equal(tokenType, account.TokenType);
        }

        [Theory]
        [InlineData("oauth")]
        [InlineData("credentials")]
        [InlineData("email")]
        public void Account_Type_AcceptsValidTypes(string validType)
        {
            // Arrange & Act
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = validType,
                Provider = "test",
                ProviderAccountId = "123"
            };

            // Assert
            Assert.Equal(validType, account.Type);
        }

        [Theory]
        [InlineData("google")]
        [InlineData("github")]
        [InlineData("microsoft")]
        [InlineData("facebook")]
        [InlineData("twitter")]
        public void Account_Provider_AcceptsValidProviders(string validProvider)
        {
            // Arrange & Act
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = "oauth",
                Provider = validProvider,
                ProviderAccountId = "123456"
            };

            // Assert
            Assert.Equal(validProvider, account.Provider);
        }

        [Fact]
        public void Account_UserNavigationProperty_CanBeSet()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}"
            };

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = "oauth",
                Provider = "google",
                ProviderAccountId = "123456",
                UserId = user.Id
            };

            // Act
            account.User = user;

            // Assert
            Assert.Equal(user, account.User);
            Assert.Equal(user.Id, account.UserId);
        }

        [Fact]
        public void Account_ExpiresAt_AcceptsUnixTimestamp()
        {
            // Arrange
            var futureTimestamp = DateTimeOffset.UtcNow.AddDays(30).ToUnixTimeSeconds();

            // Act
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = "oauth",
                Provider = "google",
                ProviderAccountId = "123456",
                ExpiresAt = futureTimestamp
            };

            // Assert
            Assert.Equal(futureTimestamp, account.ExpiresAt);
        }

        [Fact]
        public void Account_TokensAndScope_CanContainSpecialCharacters()
        {
            // Arrange
            var complexAccessToken = "ya29.a0AfH6SMC...complex_token_with_special_chars-_=";
            var complexRefreshToken = "1//04...refresh_token_with_special_chars+/=";
            var complexScope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";

            // Act
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Type = "oauth",
                Provider = "google",
                ProviderAccountId = "123456",
                AccessToken = complexAccessToken,
                RefreshToken = complexRefreshToken,
                Scope = complexScope
            };

            // Assert
            Assert.Equal(complexAccessToken, account.AccessToken);
            Assert.Equal(complexRefreshToken, account.RefreshToken);
            Assert.Equal(complexScope, account.Scope);
        }
    }
}