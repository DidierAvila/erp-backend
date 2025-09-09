using ERP.Domain.DTOs.Auth;

namespace ERP.Application.Helpers
{
    public static class AdminPermissionsHelper
    {
        /// <summary>
        /// Define todos los permisos que debe tener un usuario administrador
        /// </summary>
        public static List<CreatePermissionDto> GetAdminPermissions()
        {
            var permissions = new List<CreatePermissionDto>();

            // ========================
            // MÓDULO DE AUTENTICACIÓN
            // ========================
            
            // Gestión de Usuarios
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "users.create", Description = "Crear nuevos usuarios", Status = true },
                new CreatePermissionDto { Name = "users.read", Description = "Consultar información de usuarios", Status = true },
                new CreatePermissionDto { Name = "users.update", Description = "Actualizar información de usuarios", Status = true },
                new CreatePermissionDto { Name = "users.delete", Description = "Eliminar usuarios", Status = true },
                new CreatePermissionDto { Name = "users.change_password", Description = "Cambiar contraseñas de usuarios", Status = true },
                new CreatePermissionDto { Name = "users.manage_additional_data", Description = "Gestionar datos adicionales de usuarios", Status = true }
            });

            // Gestión de Tipos de Usuario
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "user_types.create", Description = "Crear tipos de usuario", Status = true },
                new CreatePermissionDto { Name = "user_types.read", Description = "Consultar tipos de usuario", Status = true },
                new CreatePermissionDto { Name = "user_types.update", Description = "Actualizar tipos de usuario", Status = true },
                new CreatePermissionDto { Name = "user_types.delete", Description = "Eliminar tipos de usuario", Status = true }
            });

            // Gestión de Roles
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "roles.create", Description = "Crear roles", Status = true },
                new CreatePermissionDto { Name = "roles.read", Description = "Consultar roles", Status = true },
                new CreatePermissionDto { Name = "roles.update", Description = "Actualizar roles", Status = true },
                new CreatePermissionDto { Name = "roles.delete", Description = "Eliminar roles", Status = true },
                new CreatePermissionDto { Name = "roles.assign_permissions", Description = "Asignar permisos a roles", Status = true }
            });

            // Gestión de Permisos
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "permissions.create", Description = "Crear permisos", Status = true },
                new CreatePermissionDto { Name = "permissions.read", Description = "Consultar permisos", Status = true },
                new CreatePermissionDto { Name = "permissions.update", Description = "Actualizar permisos", Status = true },
                new CreatePermissionDto { Name = "permissions.delete", Description = "Eliminar permisos", Status = true }
            });

            // ========================
            // MÓDULO FINANCIERO
            // ========================

            // Gestión de Transacciones Financieras
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "financial_transactions.create", Description = "Crear transacciones financieras", Status = true },
                new CreatePermissionDto { Name = "financial_transactions.read", Description = "Consultar transacciones financieras", Status = true },
                new CreatePermissionDto { Name = "financial_transactions.update", Description = "Actualizar transacciones financieras", Status = true },
                new CreatePermissionDto { Name = "financial_transactions.delete", Description = "Eliminar transacciones financieras", Status = true },
                new CreatePermissionDto { Name = "financial_transactions.view_summary", Description = "Ver resumen financiero", Status = true },
                new CreatePermissionDto { Name = "financial_transactions.view_by_type", Description = "Filtrar transacciones por tipo", Status = true }
            });

            // Gestión de Cuentas Financieras
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "accounts.create", Description = "Crear cuentas financieras", Status = true },
                new CreatePermissionDto { Name = "accounts.read", Description = "Consultar cuentas financieras", Status = true },
                new CreatePermissionDto { Name = "accounts.update", Description = "Actualizar cuentas financieras", Status = true },
                new CreatePermissionDto { Name = "accounts.delete", Description = "Eliminar cuentas financieras", Status = true },
                new CreatePermissionDto { Name = "accounts.view_by_type", Description = "Filtrar cuentas por tipo", Status = true },
                new CreatePermissionDto { Name = "accounts.view_active", Description = "Ver cuentas activas", Status = true }
            });

            // ========================
            // MÓDULO DE INVENTARIO
            // ========================

            // Gestión de Productos
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "products.create", Description = "Crear productos", Status = true },
                new CreatePermissionDto { Name = "products.read", Description = "Consultar productos", Status = true },
                new CreatePermissionDto { Name = "products.update", Description = "Actualizar productos", Status = true },
                new CreatePermissionDto { Name = "products.delete", Description = "Eliminar productos", Status = true },
                new CreatePermissionDto { Name = "products.view_by_sku", Description = "Buscar productos por SKU", Status = true },
                new CreatePermissionDto { Name = "products.view_low_stock", Description = "Ver productos con bajo stock", Status = true }
            });

            // Gestión de Movimientos de Stock
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "stock_movements.create", Description = "Crear movimientos de stock", Status = true },
                new CreatePermissionDto { Name = "stock_movements.read", Description = "Consultar movimientos de stock", Status = true },
                new CreatePermissionDto { Name = "stock_movements.delete", Description = "Eliminar movimientos de stock", Status = true },
                new CreatePermissionDto { Name = "stock_movements.view_by_product", Description = "Ver movimientos por producto", Status = true },
                new CreatePermissionDto { Name = "stock_movements.view_by_type", Description = "Filtrar movimientos por tipo", Status = true }
            });

            // Gestión de Ubicaciones de Inventario
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "inventory_locations.create", Description = "Crear ubicaciones de inventario", Status = true },
                new CreatePermissionDto { Name = "inventory_locations.read", Description = "Consultar ubicaciones de inventario", Status = true },
                new CreatePermissionDto { Name = "inventory_locations.update", Description = "Actualizar ubicaciones de inventario", Status = true },
                new CreatePermissionDto { Name = "inventory_locations.delete", Description = "Eliminar ubicaciones de inventario", Status = true },
                new CreatePermissionDto { Name = "inventory_locations.view_by_type", Description = "Filtrar ubicaciones por tipo", Status = true }
            });

            // ========================
            // MÓDULO DE COMPRAS
            // ========================

            // Gestión de Proveedores
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "suppliers.create", Description = "Crear proveedores", Status = true },
                new CreatePermissionDto { Name = "suppliers.read", Description = "Consultar proveedores", Status = true },
                new CreatePermissionDto { Name = "suppliers.update", Description = "Actualizar proveedores", Status = true },
                new CreatePermissionDto { Name = "suppliers.delete", Description = "Eliminar proveedores", Status = true },
                new CreatePermissionDto { Name = "suppliers.view_by_name", Description = "Buscar proveedores por nombre", Status = true }
            });

            // Gestión de Órdenes de Compra
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "purchase_orders.create", Description = "Crear órdenes de compra", Status = true },
                new CreatePermissionDto { Name = "purchase_orders.read", Description = "Consultar órdenes de compra", Status = true },
                new CreatePermissionDto { Name = "purchase_orders.update", Description = "Actualizar órdenes de compra", Status = true },
                new CreatePermissionDto { Name = "purchase_orders.delete", Description = "Eliminar órdenes de compra", Status = true },
                new CreatePermissionDto { Name = "purchase_orders.approve", Description = "Aprobar órdenes de compra", Status = true },
                new CreatePermissionDto { Name = "purchase_orders.receive", Description = "Recibir mercancía de órdenes de compra", Status = true }
            });

            // ========================
            // MÓDULO DE VENTAS
            // ========================

            // Gestión de Órdenes de Venta
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "sales_orders.create", Description = "Crear órdenes de venta", Status = true },
                new CreatePermissionDto { Name = "sales_orders.read", Description = "Consultar órdenes de venta", Status = true },
                new CreatePermissionDto { Name = "sales_orders.update", Description = "Actualizar órdenes de venta", Status = true },
                new CreatePermissionDto { Name = "sales_orders.delete", Description = "Eliminar órdenes de venta", Status = true },
                new CreatePermissionDto { Name = "sales_orders.view_by_customer", Description = "Ver órdenes por cliente", Status = true },
                new CreatePermissionDto { Name = "sales_orders.view_by_status", Description = "Filtrar órdenes por estado", Status = true },
                new CreatePermissionDto { Name = "sales_orders.process", Description = "Procesar órdenes de venta", Status = true },
                new CreatePermissionDto { Name = "sales_orders.cancel", Description = "Cancelar órdenes de venta", Status = true }
            });

            // Gestión de Clientes (mediante Users con UserType)
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "customers.create", Description = "Crear clientes", Status = true },
                new CreatePermissionDto { Name = "customers.read", Description = "Consultar información de clientes", Status = true },
                new CreatePermissionDto { Name = "customers.update", Description = "Actualizar información de clientes", Status = true },
                new CreatePermissionDto { Name = "customers.delete", Description = "Eliminar clientes", Status = true }
            });

            // ========================
            // PERMISOS ADMINISTRATIVOS
            // ========================

            // Administración del Sistema
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "system.backup", Description = "Realizar respaldos del sistema", Status = true },
                new CreatePermissionDto { Name = "system.restore", Description = "Restaurar respaldos del sistema", Status = true },
                new CreatePermissionDto { Name = "system.maintenance", Description = "Acceder al modo de mantenimiento", Status = true },
                new CreatePermissionDto { Name = "system.logs", Description = "Ver logs del sistema", Status = true },
                new CreatePermissionDto { Name = "system.settings", Description = "Configurar parámetros del sistema", Status = true }
            });

            // Reportes y Analytics
            permissions.AddRange(new[]
            {
                new CreatePermissionDto { Name = "reports.financial", Description = "Generar reportes financieros", Status = true },
                new CreatePermissionDto { Name = "reports.inventory", Description = "Generar reportes de inventario", Status = true },
                new CreatePermissionDto { Name = "reports.sales", Description = "Generar reportes de ventas", Status = true },
                new CreatePermissionDto { Name = "reports.purchases", Description = "Generar reportes de compras", Status = true },
                new CreatePermissionDto { Name = "reports.users", Description = "Generar reportes de usuarios", Status = true },
                new CreatePermissionDto { Name = "analytics.dashboard", Description = "Acceder al dashboard de analytics", Status = true }
            });

            return permissions;
        }

        /// <summary>
        /// Obtiene los permisos básicos para un rol específico
        /// </summary>
        public static List<string> GetPermissionsByRole(string roleName)
        {
            return roleName.ToLower() switch
            {
                "administrator" => GetAdminPermissions().Select(p => p.Name).ToList(),
                "manager" => GetManagerPermissions(),
                "employee" => GetEmployeePermissions(),
                "customer" => GetCustomerPermissions(),
                "supplier" => GetSupplierPermissions(),
                _ => new List<string>()
            };
        }

        private static List<string> GetManagerPermissions()
        {
            return new List<string>
            {
                // Lectura de todos los módulos
                "users.read", "user_types.read", "roles.read",
                "financial_transactions.read", "financial_transactions.view_summary",
                "accounts.read", "accounts.view_active",
                "products.read", "products.view_low_stock",
                "stock_movements.read", "stock_movements.view_by_product",
                "suppliers.read", "purchase_orders.read",
                "sales_orders.read", "sales_orders.view_by_customer", "sales_orders.view_by_status",
                "customers.read",
                
                // Creación y actualización limitada
                "products.create", "products.update",
                "stock_movements.create",
                "sales_orders.create", "sales_orders.update", "sales_orders.process",
                "purchase_orders.create", "purchase_orders.update",
                
                // Reportes
                "reports.financial", "reports.inventory", "reports.sales", "reports.purchases"
            };
        }

        private static List<string> GetEmployeePermissions()
        {
            return new List<string>
            {
                // Lectura básica
                "users.read",
                "products.read", "products.view_by_sku",
                "stock_movements.read", "stock_movements.view_by_product",
                "sales_orders.read", "sales_orders.create", "sales_orders.update",
                "customers.read", "customers.create", "customers.update",
                
                // Operaciones básicas de inventario
                "stock_movements.create"
            };
        }

        private static List<string> GetCustomerPermissions()
        {
            return new List<string>
            {
                // Solo puede ver sus propios datos y realizar pedidos
                "sales_orders.read", "sales_orders.create",
                "products.read"
            };
        }

        private static List<string> GetSupplierPermissions()
        {
            return new List<string>
            {
                // Solo puede ver información relacionada con sus productos y órdenes de compra
                "purchase_orders.read", "purchase_orders.update",
                "products.read"
            };
        }
    }
}
