using ERP.Domain.Entities.Auth;
using ERP.Domain.Entities.Finance;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Entities.Purchases;
using ERP.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.DbContexts;

public partial class ErpDbContext : DbContext
{
    public ErpDbContext()
    {
    }

    public ErpDbContext(DbContextOptions<ErpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Domain.Entities.Auth.Account> AuthAccounts { get; set; }

    public virtual DbSet<Domain.Entities.Finance.Account> FinanceAccounts { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserTypes> UserTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<InventoryLocation> InventoryLocations { get; set; }

    public virtual DbSet<StockMovement> StockMovements { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

    public virtual DbSet<FinancialTransaction> FinancialTransactions { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Auth.Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3213E83FFFBE8431");

            entity.ToTable("Accounts", "Auth");

            entity.HasIndex(e => new { e.Provider, e.ProviderAccountId }, "UQ__Accounts__6786C2C75307AB87").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            entity.Property(e => e.AccessToken)
                .HasColumnType("text")
                .HasColumnName("AccessToken");
            entity.Property(e => e.ExpiresAt).HasColumnName("ExpiresAt");
            entity.Property(e => e.IdToken)
                .HasColumnType("text")
                .HasColumnName("IdToken");
            entity.Property(e => e.Provider)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Provider");
            entity.Property(e => e.ProviderAccountId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ProviderAccountId");
            entity.Property(e => e.RefreshToken)
                .HasColumnType("text")
                .HasColumnName("RefreshToken");
            entity.Property(e => e.Scope)
                .HasColumnType("text")
                .HasColumnName("Scope");
            entity.Property(e => e.SessionState)
                .HasColumnType("text")
                .HasColumnName("SessionState");
            entity.Property(e => e.TokenType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TokenType");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Type");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Accounts__UserId__3F466844");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissions__3213E83F8986953C");

            entity.ToTable("Permissions", "Auth");

            entity.HasIndex(e => e.Name, "UQ__Permissions__72E12F1B1B3B2B3F").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("Description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("Status");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdatedAt");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83F8E286D4E");

            entity.ToTable("Roles", "Auth");

            entity.HasIndex(e => e.Name, "UQ__Roles__72E12F1B32068C24").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name");

            // Relación configurada através de la entidad RolePermission explícita
            // No necesitamos configuración muchos-a-muchos automática
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sessions__3213E83F6D1DFCA1");

            entity.ToTable("Sessions", "Auth");

            entity.HasIndex(e => e.SessionToken, "UQ__Sessions__E598F5C811A2DDCB").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            entity.Property(e => e.Expires).HasColumnName("Expires");
            entity.Property(e => e.SessionToken)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SessionToken");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Sessions__UserId__440B1D61");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F49253023");

            entity.ToTable("Users", "Auth");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616447836532").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedAt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Email");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Password");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Image");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Phone");
            entity.Property(e => e.UserTypeId)
                .HasColumnName("UserTypeId");
            entity.Property(e => e.ExtraData)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("ExtraData")
                .HasDefaultValue("{}");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UpdatedAt");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Users_UserTypes");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<UserRole>(
                    l => l.HasOne<Role>(ur => ur.Role).WithMany()
                        .HasForeignKey(ur => ur.RoleId)
                        .HasConstraintName("FK__UserRoles__RoleId__4F7CD00D"),
                    r => r.HasOne<User>(ur => ur.User).WithMany()
                        .HasForeignKey(ur => ur.UserId)
                        .HasConstraintName("FK__UserRoles__UserId__4E88ABD4"),
                    j =>
                    {
                        j.HasKey(ur => new { ur.UserId, ur.RoleId }).HasName("PK__UserRoles__6EDEA1531A203E84");
                        j.ToTable("UserRoles", "Auth");
                    });
        });

        modelBuilder.Entity<UserTypes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTypes__3213E83F");

            entity.ToTable("UserTypes", "Auth");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Name");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Description");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("Status");
        });

        modelBuilder.Entity<ERP.Domain.Entities.Finance.Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FinanceAccounts__3213E83F");

            entity.ToTable("Accounts", "Finance");

            entity.HasIndex(e => e.AccountNumber, "UQ__FinanceAccounts__AccountNumber").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");
            entity.Property(e => e.AccountName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AccountName");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AccountNumber");
            entity.Property(e => e.AccountType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AccountType");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Description");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0m)
                .HasColumnName("Balance");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IsActive");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedAt");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UpdatedAt");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            // Clave primaria compuesta
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK__RolePermissions__RoleId_PermissionId");

            entity.ToTable("RolePermissions", "Auth");

            entity.Property(e => e.RoleId)
                .HasColumnName("RoleId");
            entity.Property(e => e.PermissionId)
                .HasColumnName("PermissionId");

            entity.HasOne(d => d.Role).WithMany(r => r.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RolePermissions__Role");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RolePermissions__Permission");
        });

        // Inventory Entities Configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3213E83F");

            entity.ToTable("Products", "Inventory");

            entity.HasIndex(e => e.Sku, "UQ__Products__CA1ECF0C").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("ProductName");
            entity.Property(e => e.Sku)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("Sku");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("Description");
            entity.Property(e => e.UnitOfMeasure)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("UnitOfMeasure");
            entity.Property(e => e.CurrentStock)
                 .HasDefaultValue(0)
                 .HasColumnName("CurrentStock");
         });

         modelBuilder.Entity<InventoryLocation>(entity =>
         {
             entity.HasKey(e => e.Id).HasName("PK__InventoryLocations__3213E83F");

             entity.ToTable("InventoryLocations", "Inventory");

             entity.HasIndex(e => e.LocationName, "UQ__InventoryLocations__LocationName").IsUnique();

             entity.Property(e => e.Id)
                 .HasColumnName("Id");
             entity.Property(e => e.LocationName)
                 .HasMaxLength(255)
                 .IsRequired()
                 .HasColumnName("LocationName");
             entity.Property(e => e.Description)
                  .HasMaxLength(1000)
                  .HasColumnName("Description");
          });

          modelBuilder.Entity<StockMovement>(entity =>
          {
              entity.HasKey(e => e.Id).HasName("PK__StockMovements__3213E83F");

              entity.ToTable("StockMovements", "Inventory");

              entity.Property(e => e.Id)
                  .HasColumnName("Id");
              entity.Property(e => e.ProductId)
                  .IsRequired()
                  .HasColumnName("ProductId");
              entity.Property(e => e.MovementType)
                  .HasMaxLength(50)
                  .IsRequired()
                  .HasColumnName("MovementType");
              entity.Property(e => e.Quantity)
                  .IsRequired()
                  .HasColumnName("Quantity");
              entity.Property(e => e.MovementDate)
                  .IsRequired()
                  .HasColumnName("MovementDate");

              entity.HasOne(d => d.Product)
                  .WithMany(p => p.StockMovements)
                  .HasForeignKey(d => d.ProductId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_StockMovements_Products");

              entity.HasOne(d => d.FromLocation)
                  .WithMany(p => p.StockMovementFromLocations)
                  .HasForeignKey(d => d.FromLocationId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_StockMovements_FromLocation");

              entity.HasOne(d => d.ToLocation)
                  .WithMany(p => p.StockMovementToLocations)
                  .HasForeignKey(d => d.ToLocationId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_StockMovements_ToLocation");
          });

        // Purchases Entities Configuration
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Suppliers__3213E83F");

            entity.ToTable("Supplier");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.SupplierName)
                .IsRequired()
                .HasColumnName("SupplierName");
            entity.Property(e => e.ContactEmail)
                .IsRequired()
                .HasColumnName("ContactEmail");
            entity.Property(e => e.ContactPhone)
                .HasColumnName("ContactPhone");
            entity.Property(e => e.Address)
                .HasColumnName("Address");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PurchaseOrders__3213E83F");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.OrderDate)
                .IsRequired()
                .HasColumnName("OrderDate");
            entity.Property(e => e.SupplierId)
                .IsRequired()
                .HasColumnName("SupplierId");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("TotalAmount");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("Status");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PurchaseOrder_Supplier_SupplierId");
        });

        modelBuilder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PurchaseOrderItems__3213E83F");

            entity.ToTable("PurchaseOrderItem");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.PurchaseOrderId)
                .IsRequired()
                .HasColumnName("PurchaseOrderId");
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("ProductId");
            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("Quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("UnitPrice");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.PurchaseOrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PurchaseOrderItem_Products_ProductId");

            entity.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderItems)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderId");
        });

        // Finance Entities Configuration
        modelBuilder.Entity<FinancialTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FinancialTransactions__3213E83F");

            entity.ToTable("FinancialTransaction");

            entity.HasIndex(e => e.AccountId, "IX_FinancialTransaction_AccountId");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.TransactionType)
                .IsRequired()
                .HasColumnName("TransactionType");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("Amount");
            entity.Property(e => e.TransactionDate)
                .IsRequired()
                .HasColumnName("TransactionDate");
            entity.Property(e => e.Description)
                .HasColumnName("Description");
            entity.Property(e => e.AccountId)
                .IsRequired()
                .HasColumnName("AccountId");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt");

            entity.HasOne(d => d.Account)
                .WithMany(p => p.FinancialTransactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FinancialTransaction_Accounts_AccountId");
        });

        // Sales Entities Configuration
        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalesOrders__3213E83F");

            entity.ToTable("SalesOrder");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.OrderDate)
                .IsRequired()
                .HasColumnName("OrderDate");
            entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("TotalAmount");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("Status");
        });

        modelBuilder.Entity<SalesOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalesOrderItems__3213E83F");

            entity.ToTable("SalesOrderItem");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.SalesOrderId)
                .IsRequired()
                .HasColumnName("SalesOrderId");
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasColumnName("ProductId");
            entity.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("Quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("UnitPrice");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.SalesOrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SalesOrderItem_Products_ProductId");

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.SalesOrderItems)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SalesOrderItem_SalesOrder_SalesOrderId");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoices__3213E83F");

            entity.ToTable("Invoice");

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.InvoiceNumber)
                .IsRequired()
                .HasColumnName("InvoiceNumber");
            entity.Property(e => e.InvoiceDate)
                .IsRequired()
                .HasColumnName("InvoiceDate");
            entity.Property(e => e.DueDate)
                .IsRequired()
                .HasColumnName("DueDate");
            entity.Property(e => e.SalesOrderId)
                .HasColumnName("SalesOrderId");
            entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("TotalAmount");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("Status");

            entity.HasOne(d => d.Customer)
                .WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Invoice_Users_CustomerId");

            entity.HasOne(d => d.SalesOrder)
                .WithMany(p => p.Invoices)
                .HasForeignKey(d => d.SalesOrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Invoice_SalesOrder_SalesOrderId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
