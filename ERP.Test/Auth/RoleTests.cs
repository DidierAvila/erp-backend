using ERP.Domain.Entities.Auth;
using Xunit;

namespace ERP.Test.Auth
{
    public class RoleTests
    {
        [Fact]
        public void Role_Constructor_ShouldInitializeCollections()
        {
            // Arrange & Act
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.NotNull(role.RolePermissions);
            Assert.NotNull(role.Users);
            Assert.Empty(role.RolePermissions);
            Assert.Empty(role.Users);
        }

        [Fact]
        public void Role_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Manager";

            // Act
            var role = new Role
            {
                Id = id,
                Name = name,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(id, role.Id);
            Assert.Equal(name, role.Name);
        }

        [Fact]
        public void Role_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Employee",
                Description = null,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Null(role.Description);
        }

        [Fact]
        public void Role_OptionalProperties_CanBeSet()
        {
            // Arrange
            var description = "Full system administrator role";

            // Act
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                Description = description,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(description, role.Description);
        }

        [Theory]
        [InlineData("Administrator")]
        [InlineData("Manager")]
        [InlineData("Employee")]
        [InlineData("Viewer")]
        [InlineData("Editor")]
        public void Role_Name_AcceptsValidNames(string validName)
        {
            // Arrange & Act
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = validName,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(validName, role.Name);
        }

        [Fact]
        public void Role_Description_AcceptsLongText()
        {
            // Arrange
            var longDescription = new string('A', 500); // Máximo permitido según configuración típica

            // Act
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Test Role",
                Description = longDescription,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(longDescription, role.Description);
        }

        [Fact]
        public void Role_RolePermissions_NavigationProperty_CanAddPermissions()
        {
            // Arrange
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            var permission1 = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "users.read",
                Status = true, // Fix: Set required 'Status' property
                CreatedAt = DateTime.UtcNow // Fix: Set required 'CreatedAt' property
            };

            var permission2 = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "users.write",
                Status = true, // Fix: Set required 'Status' property
                CreatedAt = DateTime.UtcNow // Fix: Set required 'CreatedAt' property
            };

            var rolePermission1 = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission1.Id,
                Role = role,
                Permission = permission1
            };

            var rolePermission2 = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission2.Id,
                Role = role,
                Permission = permission2
            };

            // Act
            role.RolePermissions.Add(rolePermission1);
            role.RolePermissions.Add(rolePermission2);

            // Assert
            Assert.Equal(2, role.RolePermissions.Count);
            Assert.Contains(rolePermission1, role.RolePermissions);
            Assert.Contains(rolePermission2, role.RolePermissions);
        }

        [Fact]
        public void Role_Users_NavigationProperty_CanAddUsers()
        {
            // Arrange
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Name = "User 1",
                Email = "user1@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}"
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Name = "User 2",
                Email = "user2@example.com",
                UserTypeId = Guid.NewGuid(),
                ExtraData = "{}"
            };

            // Act
            role.Users.Add(user1);
            role.Users.Add(user2);

            // Assert
            Assert.Equal(2, role.Users.Count);
            Assert.Contains(user1, role.Users);
            Assert.Contains(user2, role.Users);
        }

        [Fact]
        public void Role_EmptyName_ShouldStillBeAssignable()
        {
            // Arrange & Act
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(string.Empty, role.Name);
        }
    }
}