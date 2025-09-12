using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using ERP.Infrastructure.DbContexts;
using ERP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ERP.Test.Infrastructure
{
    public class RolePermissionRepositoryTests
    {
        private readonly Mock<ErpDbContext> _mockContext;
        private readonly Mock<ILogger<RolePermissionRepository>> _mockLogger;
        private readonly RolePermissionRepository _repository;
        private readonly Mock<DbSet<RolePermission>> _mockRolePermissionDbSet;
        private readonly Mock<DbSet<Permission>> _mockPermissionDbSet;

        public RolePermissionRepositoryTests()
        {
            _mockContext = new Mock<ErpDbContext>();
            _mockLogger = new Mock<ILogger<RolePermissionRepository>>();
            _mockRolePermissionDbSet = new Mock<DbSet<RolePermission>>();
            _mockPermissionDbSet = new Mock<DbSet<Permission>>();
            
            _mockContext.Setup(c => c.Set<RolePermission>()).Returns(_mockRolePermissionDbSet.Object);
            _mockContext.Setup(c => c.Set<Permission>()).Returns(_mockPermissionDbSet.Object);
            _repository = new RolePermissionRepository(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var repository = new RolePermissionRepository(_mockContext.Object, _mockLogger.Object);

            // Assert
            Assert.NotNull(repository);
        }

        [Fact]
        public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new RolePermissionRepository(null, _mockLogger.Object));
        }

        [Fact]
        public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new RolePermissionRepository(_mockContext.Object, null));
        }

        [Fact]
        public async Task GetPermissionsByRoleIdAsync_WithValidRoleId_ShouldReturnPermissions()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var rolePermissions = new List<RolePermission>
            {
                new RolePermission { RoleId = roleId, PermissionId = Guid.NewGuid() },
                new RolePermission { RoleId = roleId, PermissionId = Guid.NewGuid() }
            }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            // Act
            var result = await _repository.GetPermissionsByRoleIdAsync(roleId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetRolesByPermissionIdAsync_WithValidPermissionId_ShouldReturnRoles()
        {
            // Arrange
            var permissionId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var rolePermissions = new List<RolePermission>
            {
                new RolePermission { RoleId = Guid.NewGuid(), PermissionId = permissionId },
                new RolePermission { RoleId = Guid.NewGuid(), PermissionId = permissionId }
            }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            // Act
            var result = await _repository.GetRolesByPermissionIdAsync(permissionId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ExistsAsync_WithValidParameters_ShouldReturnTrue()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.ExistsAsync(roleId, permissionId, cancellationToken);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemovePermissionFromRoleAsync_WithValidParameters_ShouldCompleteSuccessfully()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
            var rolePermissions = new List<RolePermission> { rolePermission }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            await _repository.RemovePermissionFromRoleAsync(roleId, permissionId, cancellationToken);

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task RemoveAllPermissionsFromRoleAsync_WithValidRoleId_ShouldCompleteSuccessfully()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var rolePermissions = new List<RolePermission>
            {
                new RolePermission { RoleId = roleId, PermissionId = Guid.NewGuid() },
                new RolePermission { RoleId = roleId, PermissionId = Guid.NewGuid() }
            }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            await _repository.RemoveAllPermissionsFromRoleAsync(roleId, cancellationToken);

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task GetRolePermissionsWithDetailsAsync_ShouldReturnRolePermissionsWithDetails()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            
            var rolePermissions = new List<RolePermission>
            {
                new RolePermission { RoleId = Guid.NewGuid(), PermissionId = Guid.NewGuid() },
                new RolePermission { RoleId = Guid.NewGuid(), PermissionId = Guid.NewGuid() }
            }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            // Act
            var result = await _repository.GetRolePermissionsWithDetailsAsync(cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByRoleAndPermissionAsync_WithValidParameters_ShouldReturnRolePermission()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
            var rolePermissions = new List<RolePermission> { rolePermission }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            // Act
            var result = await _repository.GetByRoleAndPermissionAsync(roleId, permissionId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByCompositeIdAsync_WithValidParameters_ShouldReturnRolePermission()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var permissionId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var rolePermission = new RolePermission { RoleId = roleId, PermissionId = permissionId };
            var rolePermissions = new List<RolePermission> { rolePermission }.AsQueryable();

            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Provider).Returns(rolePermissions.Provider);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.Expression).Returns(rolePermissions.Expression);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.ElementType).Returns(rolePermissions.ElementType);
            _mockRolePermissionDbSet.As<IQueryable<RolePermission>>().Setup(m => m.GetEnumerator()).Returns(rolePermissions.GetEnumerator());

            // Act
            var result = await _repository.GetByCompositeIdAsync(roleId, permissionId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }
    }
}