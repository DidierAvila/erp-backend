using ERP.Domain.Entities;
using ERP.Domain.Entities.Auth;
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

    public virtual DbSet<ERP.Domain.Entities.Auth.Account> AuthAccounts { get; set; }

    public virtual DbSet<ERP.Domain.Entities.Finance.Account> FinanceAccounts { get; set; }

    public virtual DbSet<ERP.Domain.Entities.Auth.Permission> Permissions { get; set; }

    public virtual DbSet<ERP.Domain.Entities.Auth.Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTypes> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
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
                .HasMaxLength(255)
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
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__UserRoles__RoleId__4F7CD00D"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserRoles__UserId__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRoles__6EDEA1531A203E84");
                        j.ToTable("UserRoles", "Auth");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("UserId");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("RoleId");
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
                .HasDefaultValueSql("(newid())")
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
