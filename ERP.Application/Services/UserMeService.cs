using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using System.Text.Json;

namespace ERP.Application.Services
{
    public class UserMeService
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRepositoryBase<Session> _sessionRepository;
        private readonly IRepositoryBase<Permission> _permissionRepository;
        private readonly IMapper _mapper;

        public UserMeService(
            IRepositoryBase<User> userRepository,
            IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository,
            IRepositoryBase<Role> roleRepository,
            IUserRoleRepository userRoleRepository,
            IRolePermissionRepository rolePermissionRepository,
            IRepositoryBase<Session> sessionRepository,
            IRepositoryBase<Permission> permissionRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _sessionRepository = sessionRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene la información básica del usuario autenticado y sus permisos
        /// Solo incluye campos que no estén vacíos para optimizar la respuesta
        /// </summary>
        public async Task<UserMeDto> GetUserMeAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.Find(u => u.Id == userId, cancellationToken);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            // Obtener información del tipo de usuario
            var userType = await _userTypeRepository.Find(ut => ut.Id == user.UserTypeId, cancellationToken);

            var userMe = new UserMeDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                UserTypeId = user.UserTypeId,
                CreatedAt = user.CreatedAt ?? DateTime.UtcNow,
            };

            // Solo incluir campos opcionales si no están vacíos
            if (!string.IsNullOrWhiteSpace(user.Image))
                userMe.Image = user.Image;

            if (!string.IsNullOrWhiteSpace(user.Phone))
                userMe.Phone = user.Phone;

            if (!string.IsNullOrWhiteSpace(user.Addres))
                userMe.Address = user.Addres;

            if (!string.IsNullOrWhiteSpace(userType?.Name))
                userMe.UserTypeName = userType.Name;

            if (user.UpdatedAt.HasValue)
                userMe.UpdatedAt = user.UpdatedAt;

            // Obtener roles del usuario (solo activos) - Cargar explícitamente
            userMe.Roles = await GetUserRolesOptimizedAsync(userId, cancellationToken);

            // Obtener permisos únicos del usuario (de todos sus roles activos) - Optimizado
            userMe.Permissions = await GetUserPermissionsOptimizedAsync(userId, cancellationToken);

            // Procesar datos adicionales si existen
            if (!string.IsNullOrEmpty(user.ExtraData) && user.ExtraData != "{}")
            {
                try
                {
                    userMe.AdditionalData = JsonSerializer.Deserialize<Dictionary<string, object>>(user.ExtraData);
                }
                catch
                {
                    userMe.AdditionalData = new Dictionary<string, object>();
                }
            }

            // Obtener información de la última sesión
            userMe.LastLoginAt = await GetLastLoginAsync(userId, cancellationToken);

            return userMe;
        }

        /// <summary>
        /// Método optimizado para obtener roles del usuario usando UserRoleRepository
        /// </summary>
        private async Task<List<UserRoleDto>> GetUserRolesOptimizedAsync(Guid userId, CancellationToken cancellationToken)
        {
            var roles = new List<UserRoleDto>();

            // Obtener relaciones UserRole del usuario
            var userRoles = await _userRoleRepository.GetUserRolesAsync(userId, cancellationToken);
            
            // Consultar detalles de cada rol
            foreach (var userRole in userRoles)
            {
                var role = await _roleRepository.Find(r => r.Id == userRole.RoleId, cancellationToken);
                if (role != null && role.Status)
                {
                    roles.Add(new UserRoleDto
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = !string.IsNullOrWhiteSpace(role.Description) ? role.Description : null,
                        Status = role.Status
                    });
                }
            }
            
            return roles.OrderBy(r => r.Name).ToList();
        }

        private async Task<List<UserRoleDto>> GetUserRolesAsync(User user, CancellationToken cancellationToken)
        {
            var roles = new List<UserRoleDto>();

            // Obtener roles del usuario a través de la navegación
            if (user.Roles != null && user.Roles.Any())
            {
                roles = user.Roles
                    .Where(r => r.Status) // Solo roles activos
                    .Select(r => new UserRoleDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description,
                        Status = r.Status
                    })
                    .ToList();
            }
            
            return await Task.FromResult(roles);
        }

        /// <summary>
        /// Método optimizado para obtener permisos del usuario con solo ID y Nombre
        /// Consulta directamente los roles activos sin depender de navegación
        /// </summary>
        private async Task<List<PermissionBasicDto>> GetUserPermissionsOptimizedAsync(Guid userId, CancellationToken cancellationToken)
        {
            var permissionsMap = new Dictionary<Guid, PermissionBasicDto>();

            // Obtener roles activos del usuario de forma optimizada
            var userRoles = await GetUserRolesOptimizedAsync(userId, cancellationToken);
            
            // Obtener permisos de todos los roles activos del usuario
            foreach (var userRole in userRoles)
            {
                var rolePermissions = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(userRole.Id, cancellationToken);
                foreach (var rolePermission in rolePermissions)
                {
                    // Solo obtener el permiso si no lo tenemos ya (evita duplicados)
                    if (!permissionsMap.ContainsKey(rolePermission.PermissionId))
                    {
                        var permission = await _permissionRepository.Find(p => p.Id == rolePermission.PermissionId, cancellationToken);
                        if (permission != null && permission.Status)
                        {
                            permissionsMap[permission.Id] = new PermissionBasicDto
                            {
                                Id = permission.Id,
                                Name = permission.Name
                            };
                        }
                    }
                }
            }

            return permissionsMap.Values.OrderBy(p => p.Name).ToList();
        }

        private UserConfigurationDto GetUserConfiguration(User user)
        {
            var config = new UserConfigurationDto();

            // Si el usuario tiene configuraciones personalizadas en AdditionalData
            if (!string.IsNullOrEmpty(user.ExtraData) && user.ExtraData != "{}")
            {
                try
                {
                    var extraData = JsonSerializer.Deserialize<Dictionary<string, object>>(user.ExtraData);
                    if (extraData != null && extraData.ContainsKey("configuration"))
                    {
                        var configJson = extraData["configuration"].ToString();
                        if (!string.IsNullOrEmpty(configJson))
                        {
                            var customConfig = JsonSerializer.Deserialize<UserConfigurationDto>(configJson);
                            if (customConfig != null)
                            {
                                config = customConfig;
                            }
                        }
                    }
                }
                catch
                {
                    // Si hay error, usar configuración por defecto
                }
            }

            return config;
        }

        private List<MenuItemDto> GetAvailableMenus(List<string> userPermissions)
        {
            var menus = new List<MenuItemDto>();

            // Dashboard - siempre disponible para usuarios autenticados
            menus.Add(new MenuItemDto
            {
                Id = "dashboard",
                Label = "Dashboard",
                Icon = "dashboard",
                Route = "/dashboard",
                Order = 1,
                IsGroup = false
            });

            // Módulo de Autenticación
            var authMenus = new List<MenuItemDto>();
            
            if (userPermissions.Contains("users.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "users",
                    Label = "Usuarios",
                    Icon = "people",
                    Route = "/auth/users",
                    Order = 1,
                    RequiredPermissions = new List<string> { "users.read" }
                });
            }

            if (userPermissions.Contains("roles.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "roles",
                    Label = "Roles",
                    Icon = "security",
                    Route = "/auth/roles",
                    Order = 2,
                    RequiredPermissions = new List<string> { "roles.read" }
                });
            }

            if (userPermissions.Contains("permissions.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "permissions",
                    Label = "Permisos",
                    Icon = "key",
                    Route = "/auth/permissions",
                    Order = 3,
                    RequiredPermissions = new List<string> { "permissions.read" }
                });
            }

            if (authMenus.Any())
            {
                menus.Add(new MenuItemDto
                {
                    Id = "auth",
                    Label = "Autenticación",
                    Icon = "shield",
                    Order = 2,
                    IsGroup = true,
                    Children = authMenus
                });
            }

            // Módulo Financiero
            var financeMenus = new List<MenuItemDto>();
            
            if (userPermissions.Contains("financial_transactions.read"))
            {
                financeMenus.Add(new MenuItemDto
                {
                    Id = "transactions",
                    Label = "Transacciones",
                    Icon = "attach_money",
                    Route = "/finance/transactions",
                    Order = 1,
                    RequiredPermissions = new List<string> { "financial_transactions.read" }
                });
            }

            if (financeMenus.Any())
            {
                menus.Add(new MenuItemDto
                {
                    Id = "finance",
                    Label = "Finanzas",
                    Icon = "account_balance",
                    Order = 3,
                    IsGroup = true,
                    Children = financeMenus
                });
            }

            // Módulo de Inventario
            var inventoryMenus = new List<MenuItemDto>();
            
            if (userPermissions.Contains("products.read"))
            {
                inventoryMenus.Add(new MenuItemDto
                {
                    Id = "products",
                    Label = "Productos",
                    Icon = "inventory",
                    Route = "/inventory/products",
                    Order = 1,
                    RequiredPermissions = new List<string> { "products.read" }
                });
            }

            if (inventoryMenus.Any())
            {
                menus.Add(new MenuItemDto
                {
                    Id = "inventory",
                    Label = "Inventario",
                    Icon = "warehouse",
                    Order = 4,
                    IsGroup = true,
                    Children = inventoryMenus
                });
            }

            return menus.OrderBy(m => m.Order).ToList();
        }

        private Dictionary<string, object>? GetAdditionalData(User user)
        {
            if (string.IsNullOrEmpty(user.ExtraData) || user.ExtraData == "{}")
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(user.ExtraData);
            }
            catch
            {
                return null;
            }
        }

        private async Task<DateTime?> GetLastLoginAsync(Guid userId, CancellationToken cancellationToken)
        {
            var sessions = await _sessionRepository.Finds(s => s.UserId == userId, cancellationToken);
            if (sessions != null && sessions.Any())
            {
                // Asumir que las sesiones tienen un campo CreatedAt o similar
                // return sessions.OrderByDescending(s => s.CreatedAt).FirstOrDefault()?.CreatedAt;
            }
            
            return null;
        }
    }
}
