using ERP.Application.Seeders;
using ERP.Domain.Entities;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.System
{
    public class InitializeSystemData
    {
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public InitializeSystemData(
            IRepositoryBase<Permission> permissionRepository,
            IRepositoryBase<Role> roleRepository,
            IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository,
            IRepositoryBase<User> userRepository,
            IRolePermissionRepository rolePermissionRepository)
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _userTypeRepository = userTypeRepository;
            _userRepository = userRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<bool> HandleAsync(string? adminEmail = null, string? adminPassword = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var seeder = new SystemSeeder(_permissionRepository, _roleRepository, _userTypeRepository, _rolePermissionRepository);

                // Ejecutar el seeder principal
                await seeder.SeedSystemDataAsync(cancellationToken);

                // Crear usuario administrador inicial si se proporcionaron credenciales
                if (!string.IsNullOrEmpty(adminEmail) && !string.IsNullOrEmpty(adminPassword))
                {
                    await seeder.CreateInitialAdminUserAsync(_userRepository, adminEmail, adminPassword, cancellationToken);
                }

                return true;
            }
            catch (Exception)
            {
                // En un entorno de producción, aquí se debería loggear la excepción
                throw;
            }
        }
    }
}
