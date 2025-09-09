using ERP.Application.Helpers;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Seeders
{
    public class SystemSeeder
    {
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public SystemSeeder(
            IRepositoryBase<Permission> permissionRepository,
            IRepositoryBase<Role> roleRepository,
            IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository,
            IRolePermissionRepository rolePermissionRepository)
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _userTypeRepository = userTypeRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        /// <summary>
        /// Inicializa los datos básicos del sistema: UserTypes, Roles y Permisos
        /// </summary>
        public async Task SeedSystemDataAsync(CancellationToken cancellationToken = default)
        {
            // 1. Crear tipos de usuario
            await CreateUserTypesAsync(cancellationToken);

            // 2. Crear permisos
            await CreatePermissionsAsync(cancellationToken);

            // 3. Crear roles
            await CreateRolesAsync(cancellationToken);

            // 4. Asignar permisos a roles
            await AssignPermissionsToRolesAsync(cancellationToken);
        }

        private async Task CreateUserTypesAsync(CancellationToken cancellationToken)
        {
            var userTypes = new List<ERP.Domain.Entities.Auth.UserTypes>
            {
                new ERP.Domain.Entities.Auth.UserTypes
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrator",
                    Description = "Usuario administrador del sistema con acceso completo",
                    Status = true
                },
                new ERP.Domain.Entities.Auth.UserTypes
                {
                    Id = Guid.NewGuid(),
                    Name = "Manager",
                    Description = "Usuario gerente con permisos de gestión y supervisión",
                    Status = true
                },
                new ERP.Domain.Entities.Auth.UserTypes
                {
                    Id = Guid.NewGuid(),
                    Name = "Employee",
                    Description = "Usuario empleado con permisos operativos básicos",
                    Status = true
                },
                new ERP.Domain.Entities.Auth.UserTypes
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer",
                    Description = "Usuario cliente para realizar pedidos y consultas",
                    Status = true
                },
                new ERP.Domain.Entities.Auth.UserTypes
                {
                    Id = Guid.NewGuid(),
                    Name = "Supplier",
                    Description = "Usuario proveedor para gestionar órdenes de compra",
                    Status = true
                }
            };

            foreach (var userType in userTypes)
            {
                var existing = await _userTypeRepository.Find(x => x.Name == userType.Name, cancellationToken);
                if (existing == null)
                {
                    await _userTypeRepository.Create(userType, cancellationToken);
                }
            }
        }

        private async Task CreatePermissionsAsync(CancellationToken cancellationToken)
        {
            var adminPermissions = AdminPermissionsHelper.GetAdminPermissions();

            foreach (var permissionDto in adminPermissions)
            {
                var existing = await _permissionRepository.Find(x => x.Name == permissionDto.Name, cancellationToken);
                if (existing == null)
                {
                    var permission = new Permission
                    {
                        Id = Guid.NewGuid(),
                        Name = permissionDto.Name,
                        Description = permissionDto.Description,
                        Status = permissionDto.Status,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _permissionRepository.Create(permission, cancellationToken);
                }
            }
        }

        private async Task CreateRolesAsync(CancellationToken cancellationToken)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrator",
                    Description = "Full system access with all permissions",
                    Status = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Manager",
                    Description = "Management level access with departmental permissions",
                    Status = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Employee",
                    Description = "Standard employee access with basic operational permissions",
                    Status = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Customer",
                    Description = "External customer access with limited permissions",
                    Status = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Supplier",
                    Description = "External supplier access with procurement-related permissions",
                    Status = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            foreach (var role in roles)
            {
                var existing = await _roleRepository.Find(x => x.Name == role.Name, cancellationToken);
                if (existing == null)
                {
                    await _roleRepository.Create(role, cancellationToken);
                }
            }
        }

        private async Task AssignPermissionsToRolesAsync(CancellationToken cancellationToken)
        {
            // Obtener todos los roles y permisos
            var allRoles = await _roleRepository.GetAll(cancellationToken);
            var allPermissions = await _permissionRepository.GetAll(cancellationToken);

            var permissionDict = allPermissions.ToDictionary(p => p.Name, p => p);
            var roleDict = allRoles.ToDictionary(r => r.Name, r => r);

            foreach (var role in allRoles)
            {
                // Obtener permisos para este rol
                var rolePermissionNames = AdminPermissionsHelper.GetPermissionsByRole(role.Name);
                
                // Limpiar asignaciones existentes para este rol
                await _rolePermissionRepository.RemoveAllPermissionsFromRoleAsync(role.Id, cancellationToken);

                // Crear nuevas asignaciones de permisos
                foreach (var permissionName in rolePermissionNames)
                {
                    if (permissionDict.TryGetValue(permissionName, out var permission))
                    {
                        // Verificar si ya existe la asignación
                        var exists = await _rolePermissionRepository.ExistsAsync(role.Id, permission.Id, cancellationToken);
                        if (!exists)
                        {
                            var rolePermission = new RolePermission
                            {
                                RoleId = role.Id,
                                PermissionId = permission.Id
                            };

                            await _rolePermissionRepository.Create(rolePermission, cancellationToken);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Crea el usuario administrador inicial
        /// </summary>
        public async Task CreateInitialAdminUserAsync(
            IRepositoryBase<User> userRepository,
            string adminEmail = "admin@erp.com",
            string adminPassword = "Admin123!",
            CancellationToken cancellationToken = default)
        {
            // Verificar si ya existe un administrador
            var existingAdmin = await userRepository.Find(x => x.Email == adminEmail, cancellationToken);
            if (existingAdmin != null) return;

            // Obtener el tipo de usuario Administrator
            var adminUserType = await _userTypeRepository.Find(x => x.Name == "Administrator", cancellationToken);
            if (adminUserType == null)
                throw new InvalidOperationException("UserType 'Administrator' not found. Please run SystemSeeder first.");

            // Obtener el rol Administrator
            var adminRole = await _roleRepository.Find(x => x.Name == "Administrator", cancellationToken);
            if (adminRole == null)
                throw new InvalidOperationException("Role 'Administrator' not found. Please run SystemSeeder first.");

            // Crear usuario administrador inicial
            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                Name = "System Administrator",
                Email = adminEmail,
                Password = adminPassword, // En producción, esto debe estar hasheado
                UserTypeId = adminUserType.Id,
                ExtraData = "{}",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Asignar el rol de administrador
            adminUser.Roles.Add(adminRole);

            await userRepository.Create(adminUser, cancellationToken);
        }
    }
}
