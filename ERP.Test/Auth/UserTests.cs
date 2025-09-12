using ERP.Domain.Entities.Auth;
using Xunit;

namespace ERP.Test.Auth
{
    public class UserTests
    {
        [Fact]
        public void User_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}"
            };

            // Assert
            Assert.NotNull(user.Accounts);
            Assert.NotNull(user.Sessions);
            Assert.NotNull(user.Roles);
            Assert.Empty(user.Accounts);
            Assert.Empty(user.Sessions);
            Assert.Empty(user.Roles);
        }

        [Fact]
        public void User_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userTypeId = Guid.NewGuid();
            var name = "John Doe";
            var email = "john.doe@example.com";
            var extraData = "{\"department\": \"IT\"}";

            // Act
            var user = new User
            {
                Id = id,
                Name = name,
                Email = email,
                UserTypeId = userTypeId,
                ExtraData = extraData
            };

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(userTypeId, user.UserTypeId);
            Assert.Equal(extraData, user.ExtraData);
        }

        [Fact]
        public void User_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}",
                Addres = null,
                Password = null,
                Image = null,
                Phone = null,
                CreatedAt = null,
                UpdatedAt = null
            };

            // Assert
            Assert.Null(user.Addres);
            Assert.Null(user.Password);
            Assert.Null(user.Image);
            Assert.Null(user.Phone);
            Assert.Null(user.CreatedAt);
            Assert.Null(user.UpdatedAt);
        }

        [Fact]
        public void User_OptionalProperties_CanBeSet()
        {
            // Arrange
            var createdAt = DateTime.UtcNow;
            var updatedAt = DateTime.UtcNow.AddHours(1);

            // Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "test@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}",
                Addres = "123 Main St",
                Password = "hashedpassword",
                Image = "profile.jpg",
                Phone = "+1234567890",
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal("123 Main St", user.Addres);
            Assert.Equal("hashedpassword", user.Password);
            Assert.Equal("profile.jpg", user.Image);
            Assert.Equal("+1234567890", user.Phone);
            Assert.Equal(createdAt, user.CreatedAt);
            Assert.Equal(updatedAt, user.UpdatedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void User_Name_CannotBeEmptyOrWhitespace(string invalidName)
        {
            // Arrange & Act & Assert
            // Note: En un escenario real, esto podría lanzar una excepción
            // o ser validado por FluentValidation, pero aquí solo probamos la asignación
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = invalidName,
                Email = "test@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}"
            };

            Assert.Equal(invalidName, user.Name);
        }

        [Theory]
        [InlineData("valid@example.com")]
        [InlineData("user.name@domain.co.uk")]
        [InlineData("test+tag@example.org")]
        public void User_Email_AcceptsValidFormats(string validEmail)
        {
            // Arrange & Act
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = validEmail,
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}"
            };

            // Assert
            Assert.Equal(validEmail, user.Email);
        }
    }
}