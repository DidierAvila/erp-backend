using ERP.Domain.Entities.Auth;
using Xunit;

namespace ERP.Test.Auth
{
    public class PermissionTests
    {
        [Fact]
        public void Permission_Constructor_ShouldInitializeRolePermissionsCollection()
        {
            // Arrange & Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "users.read",
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.NotNull(permission.RolePermissions);
            Assert.Empty(permission.RolePermissions);
        }

        [Fact]
        public void Permission_RequiredProperties_ShouldBeSet()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "users.write";

            // Act
            var permission = new Permission
            {
                Id = id,
                Name = name,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(id, permission.Id);
            Assert.Equal(name, permission.Name);
        }

        [Fact]
        public void Permission_OptionalProperties_CanBeNull()
        {
            // Arrange & Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "products.read",
                Description = null,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Null(permission.Description);
        }

        [Fact]
        public void Permission_OptionalProperties_CanBeSet()
        {
            // Arrange
            var description = "Allows reading user information";

            // Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "users.read",
                Description = description,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(description, permission.Description);
        }

        [Theory]
        [InlineData("users.read")]
        [InlineData("users.write")]
        [InlineData("users.delete")]
        [InlineData("products.create")]
        [InlineData("financial_transactions.read")]
        [InlineData("reports.generate")]
        public void Permission_Name_AcceptsValidPermissionNames(string validName)
        {
            // Arrange & Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = validName,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(validName, permission.Name);
        }

        [Fact]
        public void Permission_Description_AcceptsLongText()
        {
            // Arrange
            var longDescription = new string('A', 500); // Máximo permitido según configuración típica

            // Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "test.permission",
                Description = longDescription,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(longDescription, permission.Description);
        }

        [Fact]
        public void Permission_RolePermissions_NavigationProperty_CanAddRoles()
        {
            // Arrange
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "users.read",
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            var role1 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                Status = true, // Fix for CS9035: 'Role.Status' must be set
                CreatedAt = DateTime.UtcNow // Fix for CS9035: 'Role.CreatedAt' must be set
            };

            var role2 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Employee",
                Status = true, // Fix for CS9035: 'Role.Status' must be set
                CreatedAt = DateTime.UtcNow // Fix for CS9035: 'Role.CreatedAt' must be set
            };

            var rolePermission1 = new RolePermission
            {
                RoleId = role1.Id,
                PermissionId = permission.Id,
                Role = role1,
                Permission = permission
            };

            var rolePermission2 = new RolePermission
            {
                RoleId = role2.Id,
                PermissionId = permission.Id,
                Role = role2,
                Permission = permission
            };

            // Act
            permission.RolePermissions.Add(rolePermission1);
            permission.RolePermissions.Add(rolePermission2);

            // Assert
            Assert.Equal(2, permission.RolePermissions.Count);
            Assert.Contains(rolePermission1, permission.RolePermissions);
            Assert.Contains(rolePermission2, permission.RolePermissions);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Permission_Name_CanBeEmptyOrWhitespace(string name)
        {
            // Arrange & Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = name,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(name, permission.Name);
        }

        [Fact]
        public void Permission_Name_WithSpecialCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithSpecialChars = "users.read-write_admin";

            // Act
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = nameWithSpecialChars,
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(nameWithSpecialChars, permission.Name);
        }

        [Fact]
        public void Permission_MultipleRolePermissions_ShouldMaintainRelationships()
        {
            // Arrange
            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "products.manage",
                Status = true,
                CreatedAt = DateTime.UtcNow
            };

            var adminRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                Status = true, // Fix for CS9035: 'Role.Status' must be set
                CreatedAt = DateTime.UtcNow // Fix for CS9035: 'Role.CreatedAt' must be set
            };
            var managerRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                Status = true, // Fix for CS9035: 'Role.Status' must be set
                CreatedAt = DateTime.UtcNow // Fix for CS9035: 'Role.CreatedAt' must be set
            };

            var adminRolePermission = new RolePermission
            {
                RoleId = adminRole.Id,
                PermissionId = permission.Id,
                Role = adminRole,
                Permission = permission
            };

            var managerRolePermission = new RolePermission
            {
                RoleId = managerRole.Id,
                PermissionId = permission.Id,
                Role = managerRole,
                Permission = permission
            };

            // Act
            permission.RolePermissions.Add(adminRolePermission);
            permission.RolePermissions.Add(managerRolePermission);

            // Assert
            Assert.Equal(2, permission.RolePermissions.Count);
            Assert.All(permission.RolePermissions, rp => Assert.Equal(permission.Id, rp.PermissionId));
        }
    }
}