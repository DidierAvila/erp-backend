using ERP.Domain.Repositories;
using ERP.Infrastructure.DbContexts;
using ERP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace ERP.Test.Infrastructure
{
    public class RepositoryBaseTests
    {
        private readonly Mock<ErpDbContext> _mockContext;
        private readonly Mock<ILogger<RepositoryBase<TestEntity>>> _mockLogger;
        private readonly RepositoryBase<TestEntity> _repository;
        private readonly Mock<DbSet<TestEntity>> _mockDbSet;

        public RepositoryBaseTests()
        {
            _mockContext = new Mock<ErpDbContext>();
            _mockLogger = new Mock<ILogger<RepositoryBase<TestEntity>>>();
            _mockDbSet = new Mock<DbSet<TestEntity>>();
            
            _mockContext.Setup(c => c.Set<TestEntity>()).Returns(_mockDbSet.Object);
            _repository = new RepositoryBase<TestEntity>(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var repository = new RepositoryBase<TestEntity>(_mockContext.Object, _mockLogger.Object);

            // Assert
            Assert.NotNull(repository);
        }

        [Fact]
        public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new RepositoryBase<TestEntity>(null, _mockLogger.Object));
        }

        [Fact]
        public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new RepositoryBase<TestEntity>(_mockContext.Object, null));
        }

        [Fact]
        public async Task GetAll_ShouldCallToListAsync()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var testEntities = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(testEntities.Provider);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(testEntities.Expression);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(testEntities.ElementType);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(testEntities.GetEnumerator());

            // Act
            var result = await _repository.GetAll(cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByID_WithIntId_ShouldCallFindAsync()
        {
            // Arrange
            var id = 1;
            var cancellationToken = CancellationToken.None;
            var expectedEntity = new TestEntity { Id = id, Name = "Test" };

            _mockDbSet.Setup(m => m.FindAsync(id, cancellationToken))
                     .ReturnsAsync(expectedEntity);

            // Act
            var result = await _repository.GetByID(id, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.FindAsync(id, cancellationToken), Times.Once);
            Assert.Equal(expectedEntity, result);
        }

        [Fact]
        public async Task GetByID_WithGuidId_ShouldCallFindAsync()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            var expectedEntity = new TestEntity { GuidId = id, Name = "Test" };

            _mockDbSet.Setup(m => m.FindAsync(id, cancellationToken))
                     .ReturnsAsync(expectedEntity);

            // Act
            var result = await _repository.GetByID(id, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.FindAsync(id, cancellationToken), Times.Once);
            Assert.Equal(expectedEntity, result);
        }

        [Fact]
        public async Task Create_WithValidEntity_ShouldAddAndSaveChanges()
        {
            // Arrange
            var entity = new TestEntity { Name = "Test" };
            var cancellationToken = CancellationToken.None;

            _mockDbSet.Setup(m => m.Add(entity)).Returns((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TestEntity>)null);
            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.Create(entity, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.Add(entity), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task Delete_WithIntId_ShouldFindAndRemove()
        {
            // Arrange
            var id = 1;
            var cancellationToken = CancellationToken.None;
            var entityToDelete = new TestEntity { Id = id, Name = "Test" };

            _mockDbSet.Setup(m => m.FindAsync(id, cancellationToken))
                     .ReturnsAsync(entityToDelete);
            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.Delete(id, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.FindAsync(id, cancellationToken), Times.Once);
            _mockDbSet.Verify(m => m.Remove(entityToDelete), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(entityToDelete, result);
        }

        [Fact]
        public async Task Delete_WithGuidId_ShouldFindAndRemove()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cancellationToken = CancellationToken.None;
            var entityToDelete = new TestEntity { GuidId = id, Name = "Test" };

            _mockDbSet.Setup(m => m.FindAsync(id, cancellationToken))
                     .ReturnsAsync(entityToDelete);
            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            var result = await _repository.Delete(id, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.FindAsync(id, cancellationToken), Times.Once);
            _mockDbSet.Verify(m => m.Remove(entityToDelete), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(entityToDelete, result);
        }

        [Fact]
        public async Task Delete_WithEntity_ShouldRemoveAndSaveChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = 1, Name = "Test" };
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            await _repository.Delete(entity, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.Remove(entity), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Update_WithValidEntity_ShouldUpdateAndSaveChanges()
        {
            // Arrange
            var entity = new TestEntity { Id = 1, Name = "Updated Test" };
            var cancellationToken = CancellationToken.None;

            _mockContext.Setup(c => c.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

            // Act
            await _repository.Update(entity, cancellationToken);

            // Assert
            _mockDbSet.Verify(m => m.Update(entity), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Find_WithExpression_ShouldReturnFirstOrDefault()
        {
            // Arrange
            Expression<Func<TestEntity, bool>> expression = x => x.Name == "Test";
            var cancellationToken = CancellationToken.None;
            var expectedEntity = new TestEntity { Id = 1, Name = "Test" };
            var testEntities = new List<TestEntity> { expectedEntity }.AsQueryable();

            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(testEntities.Provider);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(testEntities.Expression);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(testEntities.ElementType);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(testEntities.GetEnumerator());

            // Act
            var result = await _repository.Find(expression, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Finds_WithExpression_ShouldReturnFilteredResults()
        {
            // Arrange
            Expression<Func<TestEntity, bool>> expression = x => x.Name.Contains("Test");
            var cancellationToken = CancellationToken.None;
            var testEntities = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(testEntities.Provider);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(testEntities.Expression);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(testEntities.ElementType);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(testEntities.GetEnumerator());

            // Act
            var result = await _repository.Finds(expression, cancellationToken);

            // Assert
            Assert.NotNull(result);
        }
    }

    // Clase de prueba para usar con el repositorio genérico
    public class TestEntity
    {
        public int Id { get; set; }
        public Guid GuidId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}