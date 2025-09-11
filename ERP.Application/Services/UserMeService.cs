using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Navigation;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Services
{
    public class UserMeService
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRepositoryBase<Permission> _permissionRepository;

        public UserMeService(
            IRepositoryBase<User> userRepository,
            IRepositoryBase<Role> roleRepository,
            IUserRoleRepository userRoleRepository,
            IRolePermissionRepository rolePermissionRepository,
            IRepositoryBase<Session> sessionRepository,
            IRepositoryBase<Permission> permissionRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// Obtiene la información del usuario autenticado en formato híbrido con navegación y permisos agrupados
        /// Formato optimizado para frontends con navegación dinámica y permisos granulares
        /// </summary>
        public async Task<UserMeResponseDto> GetUserMeAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
            {
                return new UserMeResponseDto
                {
                    Success = false,
                    Data = {},
                };
            }

            var user = await _userRepository.Find(u => u.Id == userId, cancellationToken);
            if (user == null)
            {
                return new UserMeResponseDto
                {
                    Success = false,
                    Data = { },
                };
            }

            // Obtener roles del usuario (solo activos)
            var userRoles = await GetUserRolesOptimizedAsync(userId, cancellationToken);

            // Obtener permisos únicos del usuario (de todos sus roles activos)
            var userPermissions = await GetUserPermissionsOptimizedAsync(userId, cancellationToken);

            // Generar permisos agrupados por recurso (formato híbrido)
            var permissionNames = userPermissions.Select(p => p.Name).ToList();
            var permissionsByResource = GeneratePermissionsByResource(permissionNames);

            // Generar navegación dinámica basada en permisos
            var navigation = GetAvailableMenus(permissionNames);

            // Crear respuesta en formato híbrido
            return new UserMeResponseDto
            {
                Success = true,
                Data = new UserMeDataDto
                {
                    User = new UserBasicInfoDto
                    {
                        Id = user.Id.ToString(),
                        Name = user.Name,
                        Email = user.Email,
                        Role = userRoles.FirstOrDefault()?.Name ?? "Sin Rol",
                        RoleId = userRoles.FirstOrDefault()?.Id.ToString() ?? "0",
                        Avatar = user.Image ?? "/images/users/default.jpg"
                    },
                    Navigation = navigation.Select(menu => MapMenuToNavigation(menu)).ToList(),
                    Permissions = ConvertToResourcePermissions(permissionsByResource)
                }
            };
        }

        // Conversión en línea de MenuItemDto a NavigationItemDto (recursiva)
        private NavigationItemDto MapMenuToNavigation(MenuItemDto menu)
        {
            return new NavigationItemDto
            {
                Id = menu.Id,
                Label = menu.Label,
                Icon = menu.Icon ?? string.Empty,
                Route = menu.Route,
                Order = menu.Order,
                Visible = true,
                Children = menu.Children != null ? menu.Children.Select(MapMenuToNavigation).ToList() : new List<NavigationItemDto>()
            };
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

            #region Módulo de Auth

            var authMenus = new List<MenuItemDto>();
            
            if (userPermissions.Contains("users.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "users",
                    Label = "Usuarios",
                    Icon = "people",
                    Route = "/users",
                    Order = 1,
                    RequiredPermissions = ["users.read"]
                });
            }

            if (userPermissions.Contains("roles.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "roles",
                    Label = "Roles",
                    Icon = "security",
                    Route = "/roles",
                    Order = 2,
                    RequiredPermissions = ["roles.read"]
                });
            }

            if (userPermissions.Contains("permissions.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "permissions",
                    Label = "Permisos",
                    Icon = "key",
                    Route = "/permissions",
                    Order = 3,
                    RequiredPermissions = ["permissions.read"]
                });
            }

            if (userPermissions.Contains("user_types.read"))
            {
                authMenus.Add(new MenuItemDto
                {
                    Id = "user_types",
                    Label = "Tipos de usuarios",
                    Icon = "people",
                    Route = "/user-types",
                    Order = 4,
                    RequiredPermissions = ["permissions.read"]
                });
            }
            
            if (authMenus.Any())
            {
                menus.Add(new MenuItemDto
                {
                    Id = "auth",
                    Label = "Administración",
                    Icon = "shield",
                    Order = 2,
                    IsGroup = true,
                    Children = authMenus
                });
            }
            #endregion

            #region Módulo Financiero
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

            if (userPermissions.Contains("financial_accounts.read"))
            {
                financeMenus.Add(new MenuItemDto
                {
                    Id = "account",
                    Label = "Cuentas",
                    Icon = "account_balance",
                    Route = "/finance/accounts",
                    Order = 2,
                    RequiredPermissions = new List<string> { "financial_accounts.read" }
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
            #endregion


            #region Módulo de Inventario
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
                    RequiredPermissions = ["products.read"]
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
            #endregion

            #region Módulo de Ventas
            var salesMenus = new List<MenuItemDto>();
            
            if (userPermissions.Contains("sales_orders.read"))
            {
                salesMenus.Add(new MenuItemDto
                {
                    Id = "sales_orders",
                    Label = "Órdenes de Venta",
                    Icon = "receipt",
                    Route = "/sales/orders",
                    Order = 1,
                    RequiredPermissions = ["sales_orders.read"]
                });
            }

            if (userPermissions.Contains("sales_invoices.read"))
            {
                salesMenus.Add(new MenuItemDto
                {
                    Id = "sales_invoices",
                    Label = "Factura",
                    Icon = "library_books",
                    Route = "/sales/Invoices",
                    Order = 2,
                    RequiredPermissions = ["sales_invoices.read"]
                });
            }

            if (salesMenus.Any())
            {
                menus.Add(new MenuItemDto
                {
                    Id = "sales",
                    Label = "Ventas",
                    Icon = "point_of_sale",
                    Order = 5,
                    IsGroup = true,
                    Children = salesMenus
                });
            }
            #endregion

            // Módulo de Compras
            var purchasesMenus = new List<MenuItemDto>();
            
            if (userPermissions.Contains("purchases.read"))
            {
                purchasesMenus.Add(new MenuItemDto
                {
                    Id = "purchase_orders",
                    Label = "Órdenes de Compra",
                    Icon = "shopping_cart",
                    Route = "/purchases/orders",
                    Order = 1,
                    RequiredPermissions = new List<string> { "purchases.read" }
                });
            }

            if (userPermissions.Contains("suppliers.read"))
            {
                purchasesMenus.Add(new MenuItemDto
                {
                    Id = "suppliers",
                    Label = "Proveedores",
                    Icon = "local_shipping",
                    Route = "/suppliers",
                    Order = 2,
                    RequiredPermissions = new List<string> { "suppliers.read" }
                });
            }

            if (userPermissions.Contains("purchases.read"))
            {
                purchasesMenus.Add(new MenuItemDto
                {
                    Id = "purchase_reports",
                    Label = "Reportes de Compras",
                    Icon = "analytics",
                    Route = "/purchases/reports",
                    Order = 3,
                    RequiredPermissions = new List<string> { "purchases.read" }
                });
            }

            if (purchasesMenus.Any())
            {
                menus.Add(new MenuItemDto
                {
                    Id = "purchases",
                    Label = "Compras",
                    Icon = "shopping_bag",
                    Order = 6,
                    IsGroup = true,
                    Children = purchasesMenus
                });
            }

            return menus.OrderBy(m => m.Order).ToList();
        }

        /// <summary>
        /// Genera permisos agrupados por recurso en formato híbrido para el frontend
        /// Convierte de "users.read" a { "users": { "read": true, "create": false, ... } }
        /// </summary>
        private static Dictionary<string, Dictionary<string, bool>> GeneratePermissionsByResource(List<string> userPermissions)
        {
            var permissionsByResource = new Dictionary<string, Dictionary<string, bool>>();

            // Definir recursos principales del ERP
            var resources = new[] 
            {
                "users", "roles", "permissions", "products", "sales", "customers", 
                "purchases", "suppliers", "finance", "accounts", "inventory", 
                "categories", "stock_movements", "financial_transactions", "reports"
            };

            // Definir acciones estándar
            var actions = new[] { "read", "create", "edit", "delete", "export", "import" };

            foreach (var resource in resources)
            {
                var resourcePermissions = new Dictionary<string, bool>();
                
                foreach (var action in actions)
                {
                    // Verificar si el usuario tiene el permiso específico
                    var permissionName = $"{resource}.{action}";
                    resourcePermissions[action] = userPermissions.Contains(permissionName);
                }

                // Solo incluir el recurso si tiene al menos un permiso
                if (resourcePermissions.Any(rp => rp.Value))
                {
                    permissionsByResource[resource] = resourcePermissions;
                }
            }

            return permissionsByResource;
        }


        /// <summary>
        /// Convierte el formato de permisos agrupados a ResourcePermissionsDto
        /// </summary>
        private Dictionary<string, ResourcePermissionsDto> ConvertToResourcePermissions(Dictionary<string, Dictionary<string, bool>> permissionsByResource)
        {
            var result = new Dictionary<string, ResourcePermissionsDto>();
            
            foreach (var resource in permissionsByResource)
            {
                var resourcePermissions = resource.Value;
                result[resource.Key] = new ResourcePermissionsDto
                {
                    Read = resourcePermissions.GetValueOrDefault("read", false),
                    Create = resourcePermissions.GetValueOrDefault("create", false),
                    Edit = resourcePermissions.GetValueOrDefault("edit", false),
                    Delete = resourcePermissions.GetValueOrDefault("delete", false),
                    Export = resourcePermissions.GetValueOrDefault("export", false),
                    Import = resourcePermissions.GetValueOrDefault("import", false)
                };
            }
            
            return result;
        }
    }
}
