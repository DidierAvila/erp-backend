using ERP.Application.Core.Auth.Commands.Authentication;
using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Commands.Roles;
using ERP.Application.Core.Auth.Commands.Users;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Application.Core.Auth.Queries.Roles;
using ERP.Application.Core.Auth.Queries.Users;
using ERP.Application.Core.Finance.Commands.FinancialTransactions;
using ERP.Application.Core.Finance.Commands.Handlers;
using ERP.Application.Core.Finance.Queries.FinancialTransactions;
using ERP.Application.Core.Finance.Queries.Handlers;
using ERP.Application.Core.Inventory.Commands.Handlers;
using ERP.Application.Core.Inventory.Commands.InventoryLocations;
using ERP.Application.Core.Inventory.Commands.Products;
using ERP.Application.Core.Inventory.Commands.StockMovements;
using ERP.Application.Core.Inventory.Queries.Handlers;
using ERP.Application.Core.Inventory.Queries.InventoryLocations;
using ERP.Application.Core.Inventory.Queries.Products;
using ERP.Application.Core.Inventory.Queries.StockMovements;
using ERP.Application.Core.Purchases.Commands.Suppliers;
using ERP.Application.Core.Purchases.Commands.Handlers;
using ERP.Application.Core.Purchases.Queries.Suppliers;
using ERP.Application.Core.Purchases.Queries.Handlers;
using ERP.Application.Core.Sales.Commands.Handlers;
using ERP.Application.Core.Sales.Commands.SalesOrders;
using ERP.Application.Core.Sales.Queries.Handlers;
using ERP.Application.Core.Sales.Queries.SalesOrders;
using ERP.Application.Mappings;
using ERP.Domain.Entities;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Entities.Sales;

using ERP.Domain.Repositories;
using ERP.Infrastructure.Repositories;

namespace ERP.API.Extensions
{
    public static class ExtencionServices
    {
        public static IServiceCollection AddApiErpExtention(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(UserProfile), typeof(UserTypesProfile), typeof(UserTypeProfile), typeof(PermissionProfile), typeof(AuthProfile), typeof(RoleProfile), typeof(RolePermissionProfile), typeof(ProductProfile), typeof(StockMovementProfile), typeof(FinancialTransactionProfile), typeof(AccountProfile), typeof(SupplierProfile), typeof(SalesOrderProfile));

            // Authentication Commands
            services.AddScoped<ILoginCommand, LoginCommand>();

            // User Commands
            services.AddScoped<CreateUser>();
            services.AddScoped<UpdateUser>();
            services.AddScoped<DeleteUser>();
            services.AddScoped<ChangePassword>();
            services.AddScoped<UpdateUserAdditionalData>();
            services.AddScoped<IUserCommandHandler, UserCommandHandler>();

            // User Queries  
            services.AddScoped<GetUserById>();
            services.AddScoped<GetAllUsers>();
            services.AddScoped<IUserQueryHandler, UserQueryHandler>();

            // UserType Commands
            services.AddScoped<ERP.Application.Core.Auth.Commands.UserTypes.CreateUserType>();
            services.AddScoped<ERP.Application.Core.Auth.Commands.UserTypes.UpdateUserType>();
            services.AddScoped<ERP.Application.Core.Auth.Commands.UserTypes.DeleteUserType>();
            services.AddScoped<IUserTypeCommandHandler, UserTypeCommandHandler>();

            // UserType Queries
            services.AddScoped<ERP.Application.Core.Auth.Queries.UserTypes.GetUserTypeById>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.UserTypes.GetAllUserTypes>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.UserTypes.GetActiveUserTypes>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.UserTypes.GetUserTypesSummary>();
            services.AddScoped<IUserTypeQueryHandler, UserTypeQueryHandler>();

            // Permission Commands
            services.AddScoped<ERP.Application.Core.Auth.Commands.Permissions.CreatePermission>();
            services.AddScoped<ERP.Application.Core.Auth.Commands.Permissions.UpdatePermission>();
            services.AddScoped<ERP.Application.Core.Auth.Commands.Permissions.DeletePermission>();
            services.AddScoped<IPermissionCommandHandler, PermissionCommandHandler>();

            // Permission Queries
            services.AddScoped<ERP.Application.Core.Auth.Queries.Permissions.GetPermissionById>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.Permissions.GetAllPermissions>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.Permissions.GetActivePermissions>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.Permissions.GetPermissionsSummary>();
            services.AddScoped<IPermissionQueryHandler, PermissionQueryHandler>();

            // System Commands
            services.AddScoped<ERP.Application.Core.Auth.Commands.System.InitializeSystemData>();

            // Role Commands
            services.AddScoped<CreateRole>();
            services.AddScoped<UpdateRole>();
            services.AddScoped<DeleteRole>();
            services.AddScoped<IRoleCommandHandler, RoleCommandHandler>();

            // Role Queries  
            services.AddScoped<GetRoleById>();
            services.AddScoped<GetAllRoles>();
            services.AddScoped<IRoleQueryHandler, RoleQueryHandler>();

            // RolePermission Commands
            services.AddScoped<ERP.Application.Core.Auth.Commands.RolePermissions.AssignPermissionToRole>();
            services.AddScoped<ERP.Application.Core.Auth.Commands.RolePermissions.RemovePermissionFromRole>();

            // RolePermission Queries
            services.AddScoped<ERP.Application.Core.Auth.Queries.RolePermissions.GetAllRolePermissions>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.RolePermissions.GetPermissionsByRole>();
            services.AddScoped<ERP.Application.Core.Auth.Queries.RolePermissions.GetRolesByPermission>();

            // Product Commands (Inventory)
            services.AddScoped<CreateProduct>();
            services.AddScoped<UpdateProduct>();
            services.AddScoped<DeleteProduct>();
            services.AddScoped<IProductCommandHandler, ProductCommandHandler>();

            // Product Queries (Inventory)
            services.AddScoped<GetProductById>();
            services.AddScoped<GetProductsBySku>();
            services.AddScoped<GetAllProducts>();
            services.AddScoped<GetLowStockProducts>();
            services.AddScoped<IProductQueryHandler, ProductQueryHandler>();

            // StockMovement Commands (Inventory)
            services.AddScoped<CreateStockMovement>();
            services.AddScoped<DeleteStockMovement>();
            services.AddScoped<IStockMovementCommandHandler, StockMovementCommandHandler>();

            // StockMovement Queries (Inventory)
            services.AddScoped<GetStockMovementById>();
            services.AddScoped<GetAllStockMovements>();
            services.AddScoped<GetStockMovementsByProduct>();
            services.AddScoped<GetStockMovementsByType>();
            services.AddScoped<IStockMovementQueryHandler, StockMovementQueryHandler>();

            // InventoryLocation Commands (Inventory) - Comentado temporalmente hasta completar entidad
            // services.AddScoped<CreateInventoryLocation>();
            // services.AddScoped<UpdateInventoryLocation>();
            // services.AddScoped<DeleteInventoryLocation>();
            // services.AddScoped<IInventoryLocationCommandHandler, InventoryLocationCommandHandler>();

            // InventoryLocation Queries (Inventory) - Comentado temporalmente hasta completar entidad
            // services.AddScoped<GetInventoryLocationById>();
            // services.AddScoped<GetAllInventoryLocations>();
            // services.AddScoped<GetLocationsByType>();
            // services.AddScoped<GetLocationsByParent>();
            // services.AddScoped<IInventoryLocationQueryHandler, InventoryLocationQueryHandler>();

            // FinancialTransaction Commands (Finance)
            services.AddScoped<CreateFinancialTransaction>();
            services.AddScoped<UpdateFinancialTransaction>();
            services.AddScoped<DeleteFinancialTransaction>();
            services.AddScoped<IFinancialTransactionCommandHandler, FinancialTransactionCommandHandler>();

            // FinancialTransaction Queries (Finance)
            services.AddScoped<GetFinancialTransactionById>();
            services.AddScoped<GetAllFinancialTransactions>();
            services.AddScoped<GetTransactionsByType>();
            services.AddScoped<GetFinancialSummary>();
            services.AddScoped<IFinancialTransactionQueryHandler, FinancialTransactionQueryHandler>();

            // Account Commands (Finance)
            services.AddScoped<ERP.Application.Core.Finance.Commands.Accounts.CreateAccount>();
            services.AddScoped<ERP.Application.Core.Finance.Commands.Accounts.UpdateAccount>();
            services.AddScoped<ERP.Application.Core.Finance.Commands.Accounts.DeleteAccount>();
            services.AddScoped<ERP.Application.Core.Finance.Commands.Handlers.IAccountCommandHandler, ERP.Application.Core.Finance.Commands.Handlers.AccountCommandHandler>();

            // Account Queries (Finance)
            services.AddScoped<ERP.Application.Core.Finance.Queries.Accounts.GetAccountById>();
            services.AddScoped<ERP.Application.Core.Finance.Queries.Accounts.GetAllAccounts>();
            services.AddScoped<ERP.Application.Core.Finance.Queries.Accounts.GetAccountsByType>();
            services.AddScoped<ERP.Application.Core.Finance.Queries.Accounts.GetActiveAccounts>();
            services.AddScoped<ERP.Application.Core.Finance.Queries.Handlers.IAccountQueryHandler, ERP.Application.Core.Finance.Queries.Handlers.AccountQueryHandler>();

            // Supplier Commands (Purchases)
            services.AddScoped<CreateSupplier>();
            services.AddScoped<UpdateSupplier>();
            services.AddScoped<DeleteSupplier>();
            services.AddScoped<ISupplierCommandHandler, SupplierCommandHandler>();

            // Supplier Queries (Purchases)
            services.AddScoped<GetSupplierById>();
            services.AddScoped<GetAllSuppliers>();
            // services.AddScoped<GetSuppliersByName>(); // Comentado temporalmente
            services.AddScoped<ISupplierQueryHandler, SupplierQueryHandler>();

            // SalesOrder Commands (Sales)
            services.AddScoped<CreateSalesOrder>();
            services.AddScoped<UpdateSalesOrder>();
            services.AddScoped<DeleteSalesOrder>();
            services.AddScoped<ISalesOrderCommandHandler, SalesOrderCommandHandler>();

            // SalesOrder Queries (Sales)
            services.AddScoped<GetSalesOrderById>();
            services.AddScoped<GetAllSalesOrders>();
            services.AddScoped<GetSalesOrdersByCustomer>();
            services.AddScoped<GetSalesOrdersByStatus>();
            services.AddScoped<ISalesOrderQueryHandler, SalesOrderQueryHandler>();

            // Repositories
            services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
            services.AddScoped<IRepositoryBase<Session>, RepositoryBase<Session>>();
            services.AddScoped<IRepositoryBase<Role>, RepositoryBase<Role>>();
            services.AddScoped<IRepositoryBase<Permission>, RepositoryBase<Permission>>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRepositoryBase<Product>, RepositoryBase<Product>>();
            services.AddScoped<IRepositoryBase<StockMovement>, RepositoryBase<StockMovement>>();
            // services.AddScoped<IRepositoryBase<InventoryLocation>, RepositoryBase<InventoryLocation>>(); // Comentado hasta completar entidad
            services.AddScoped<IRepositoryBase<FinancialTransaction>, RepositoryBase<FinancialTransaction>>();
            services.AddScoped<IRepositoryBase<ERP.Domain.Entities.Finance.Account>, RepositoryBase<ERP.Domain.Entities.Finance.Account>>();
            services.AddScoped<IRepositoryBase<Supplier>, RepositoryBase<Supplier>>();
            services.AddScoped<IRepositoryBase<SalesOrder>, RepositoryBase<SalesOrder>>();
            services.AddScoped<IRepositoryBase<UserTypes>, RepositoryBase<UserTypes>>();

            return services;
        }
    }
}
