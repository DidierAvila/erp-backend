using ERP.Domain.Entities.Auth;
using Xunit;

namespace ERP.Test.Auth
{
    public class UserTypesTests
    {
        [Fact]
        public void UserTypes_Constructor_ShouldInitializeUsersCollection()
        {
            // Arrange & Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Administrator"
            };

            // Assert
            Assert.NotNull(userType.Users);
            Assert.Empty(userType.Users);
        }

        [Fact]
        public void UserTypes_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Manager";

            // Act
            var userType = new UserTypes
            {
                Id = id,
                Name = name
            };

            // Assert
            Assert.Equal(id, userType.Id);
            Assert.Equal(name, userType.Name);
        }

        [Fact]
        public void UserTypes_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Employee",
                Description = null
            };

            // Assert
            Assert.Null(userType.Description);
        }

        [Fact]
        public void UserTypes_OptionalProperties_CanBeSet()
        {
            // Arrange
            var description = "System administrator with full access";

            // Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                Description = description
            };

            // Assert
            Assert.Equal(description, userType.Description);
        }

        [Fact]
        public void UserTypes_Status_DefaultValue_ShouldBeTrue()
        {
            // Arrange & Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Customer"
            };

            // Assert
            // Note: El valor por defecto se establece en la base de datos, 
            // aquí solo verificamos que se puede asignar
            Assert.True(userType.Status);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UserTypes_Status_CanBeSetToAnyBooleanValue(bool status)
        {
            // Arrange & Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Test Type",
                Status = status
            };

            // Assert
            Assert.Equal(status, userType.Status);
        }

        [Theory]
        [InlineData("Administrator")]
        [InlineData("Manager")]
        [InlineData("Employee")]
        [InlineData("Customer")]
        [InlineData("Supplier")]
        public void UserTypes_Name_AcceptsValidNames(string validName)
        {
            // Arrange & Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = validName
            };

            // Assert
            Assert.Equal(validName, userType.Name);
        }

        [Fact]
        public void UserTypes_Description_AcceptsLongText()
        {
            // Arrange
            var longDescription = new string('A', 500); // Máximo permitido según la configuración

            // Act
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Test Type",
                Description = longDescription
            };

            // Assert
            Assert.Equal(longDescription, userType.Description);
        }

        [Fact]
        public void UserTypes_Users_NavigationProperty_CanAddUsers()
        {
            // Arrange
            var userType = new UserTypes
            {
                Id = Guid.NewGuid(),
                Name = "Manager"
            };

            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Name = "User 1",
                Email = "user1@example.com",
                UserTypeId = userType.Id,
                ExtraData = "{}"
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Name = "User 2",
                Email = "user2@example.com",
                UserTypeId = userType.Id,
                ExtraData = "{}"
            };

            // Act
            userType.Users.Add(user1);
            userType.Users.Add(user2);

            // Assert
            Assert.Equal(2, userType.Users.Count);
            Assert.Contains(user1, userType.Users);
            Assert.Contains(user2, userType.Users);
        }
    }
}