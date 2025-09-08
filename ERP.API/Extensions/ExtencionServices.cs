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
using ERP.Domain.Entities.App;
using ERP.Domain.Repositories;
using ERP.Infrastructure.Repositories;

namespace ERP.API.Extensions
{
    public static class ExtencionServices
    {
        public static IServiceCollection AddApiErpExtention(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(UserProfile), typeof(AuthProfile), typeof(RoleProfile), typeof(ProductProfile), typeof(StockMovementProfile), typeof(FinancialTransactionProfile), typeof(SupplierProfile), typeof(SalesOrderProfile));

            // Authentication Commands
            services.AddScoped<ILoginCommand, LoginCommand>();

            // User Commands
            services.AddScoped<CreateUser>();
            services.AddScoped<UpdateUser>();
            services.AddScoped<DeleteUser>();
            services.AddScoped<IUserCommandHandler, UserCommandHandler>();

            // User Queries  
            services.AddScoped<GetUserById>();
            services.AddScoped<GetAllUsers>();
            services.AddScoped<IUserQueryHandler, UserQueryHandler>();

            // Role Commands
            services.AddScoped<CreateRole>();
            services.AddScoped<UpdateRole>();
            services.AddScoped<DeleteRole>();
            services.AddScoped<IRoleCommandHandler, RoleCommandHandler>();

            // Role Queries  
            services.AddScoped<GetRoleById>();
            services.AddScoped<GetAllRoles>();
            services.AddScoped<IRoleQueryHandler, RoleQueryHandler>();

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
            services.AddScoped<IRepositoryBase<Product>, RepositoryBase<Product>>();
            services.AddScoped<IRepositoryBase<StockMovement>, RepositoryBase<StockMovement>>();
            // services.AddScoped<IRepositoryBase<InventoryLocation>, RepositoryBase<InventoryLocation>>(); // Comentado hasta completar entidad
            services.AddScoped<IRepositoryBase<FinancialTransaction>, RepositoryBase<FinancialTransaction>>();
            services.AddScoped<IRepositoryBase<Supplier>, RepositoryBase<Supplier>>();
            services.AddScoped<IRepositoryBase<SalesOrder>, RepositoryBase<SalesOrder>>();
            services.AddScoped<IRepositoryBase<Customer>, RepositoryBase<Customer>>();

            return services;
        }
    }
}
