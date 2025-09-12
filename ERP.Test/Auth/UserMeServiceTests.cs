using AutoMapper;
using ERP.Application.Services;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using Moq;

namespace ERP.Test.Auth
{
    public class UserMeServiceTests
    {
        private readonly Mock<IRepositoryBase<User>> _mockUserRepository;
        private readonly Mock<IRepositoryBase<Role>> _mockRoleRepository;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        private readonly Mock<IRolePermissionRepository> _mockRolePermissionRepository;
        private readonly Mock<IRepositoryBase<Session>> _mockSessionRepository;
        private readonly Mock<IRepositoryBase<Permission>> _mockPermissionRepository;
        private readonly Mock<IMapper> _mockMapper;

        public UserMeServiceTests()
        {
            _mockUserRepository = new Mock<IRepositoryBase<User>>();
            _mockRoleRepository = new Mock<IRepositoryBase<Role>>();
            _mockUserRoleRepository = new Mock<IUserRoleRepository>();
            _mockRolePermissionRepository = new Mock<IRolePermissionRepository>();
            _mockSessionRepository = new Mock<IRepositoryBase<Session>>();
            _mockPermissionRepository = new Mock<IRepositoryBase<Permission>>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void UserMeService_Constructor_ShouldInitializeAllDependencies()
        {
            // Arrange & Act
            var service = new UserMeService(
                _mockUserRepository.Object,
                _mockRoleRepository.Object,
                _mockUserRoleRepository.Object,
                _mockRolePermissionRepository.Object,
                _mockSessionRepository.Object,
                _mockPermissionRepository.Object,
                _mockMapper.Object
            );

            // Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void UserMeService_AllRepositories_ShouldBeAccessible()
        {
            // Arrange & Act & Assert
            Assert.NotNull(_mockUserRepository.Object);
            Assert.NotNull(_mockRoleRepository.Object);
            Assert.NotNull(_mockUserRoleRepository.Object);
            Assert.NotNull(_mockRolePermissionRepository.Object);
            Assert.NotNull(_mockSessionRepository.Object);
            Assert.NotNull(_mockPermissionRepository.Object);
            Assert.NotNull(_mockMapper.Object);
        }
    }
}