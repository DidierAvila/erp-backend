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
    public class UserRoleRepositoryTests
    {
        private readonly Mock<ErpDbContext> _mockContext;
        private readonly Mock<ILogger<UserRoleRepository>> _mockLogger;
        private readonly UserRoleRepository _repository;
        private readonly Mock<DbSet<UserRole>> _mockUserRoleDbSet;
        private readonly Mock<DbSet<Role>> _mockRoleDbSet;

        public UserRoleRepositoryTests()
        {
            _mockContext = new Mock<ErpDbContext>();
            _mockLogger = new Mock<ILogger<UserRoleRepository>>();
            _mockUserRoleDbSet = new Mock<DbSet<UserRole>>();
            _mockRoleDbSet = new Mock<DbSet<Role>>();
            
            _mockContext.Setup(c => c.Set<UserRole>()).Returns(_mockUserRoleDbSet.Object);
            _mockContext.Setup(c => c.Set<Role>()).Returns(_mockRoleDbSet.Object);
            _repository = new UserRoleRepository(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var repository = new UserRoleRepository(_mockContext.Object, _mockLogger.Object);

            // Assert
            Assert.NotNull(repository);
        }

        [Fact]
        public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new UserRoleRepository(null, _mockLogger.Object));
        }

        [Fact]
        public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new UserRoleRepository(_mockContext.Object, null));
        }

        [Fact]
        public async Task GetUserRolesAsync_WithValidUserId_ShouldReturnUserRoles()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var userRoles = new List<UserRole>
            {
                new UserRole { UserId = userId, RoleId = Guid.NewGuid() },
                new UserRole { UserId = userId, RoleId = Guid.NewGuid() }
            }.AsQueryable();

            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.Provider).Returns(userRoles.Provider);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.Expression).Returns(userRoles.Expression);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.ElementType).Returns(userRoles.ElementType);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.GetEnumerator()).Returns(userRoles.GetEnumerator());

            // Act
            var result = await _repository.GetUserRolesAsync(userId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserRolesAsync_WithEmptyUserId_ShouldReturnEmptyList()
        {
            // Arrange
            var userId = Guid.Empty;
            var cancellationToken = CancellationToken.None;
            
            var emptyUserRoles = new List<UserRole>().AsQueryable();

            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.Provider).Returns(emptyUserRoles.Provider);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.Expression).Returns(emptyUserRoles.Expression);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.ElementType).Returns(emptyUserRoles.ElementType);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.GetEnumerator()).Returns(emptyUserRoles.GetEnumerator());

            // Act
            var result = await _repository.GetUserRolesAsync(userId, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task AssignRoleToUserAsync_WithValidParameters_ShouldReturnTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.AssignRoleToUserAsync(userId, roleId, cancellationToken);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveRoleFromUserAsync_WithValidParameters_ShouldReturnTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.RemoveRoleFromUserAsync(userId, roleId, cancellationToken);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetRoleUsersAsync_WithValidRoleId_ShouldReturnUsers()
        {
            // Arrange
            var roleId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            
            var userRoles = new List<UserRole>
            {
                new UserRole { UserId = Guid.NewGuid(), RoleId = roleId },
                new UserRole { UserId = Guid.NewGuid(), RoleId = roleId }
            }.AsQueryable();

            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.Provider).Returns(userRoles.Provider);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.Expression).Returns(userRoles.Expression);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.ElementType).Returns(userRoles.ElementType);
            _mockUserRoleDbSet.As<IQueryable<UserRole>>().Setup(m => m.GetEnumerator()).Returns(userRoles.GetEnumerator());

            // Act
            var result = await _repository.GetRoleUsersAsync(roleId, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UserHasRoleAsync_WithExistingUserRole_ShouldReturnTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.UserHasRoleAsync(userId, roleId, cancellationToken);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveAllUserRolesAsync_WithValidUserId_ShouldReturnTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.RemoveAllUserRolesAsync(userId, cancellationToken);

            // Assert
            Assert.True(result);
        }
    }
}