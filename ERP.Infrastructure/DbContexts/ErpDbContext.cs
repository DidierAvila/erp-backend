using ERP.Domain.Entities;
using ERP.Domain.Entities.App;
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

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Advisor> Advisors { get; set; }

    public virtual DbSet<Assistant> Assistants { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

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

        modelBuilder.Entity<Advisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Advisors__3213E83FBDE3B613");

            entity.ToTable("Advisors", "App");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");
            entity.Property(e => e.Certifications)
                .HasColumnType("text")
                .HasColumnName("Certifications");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedAt");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LastName");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Phone");
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Specialization");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UpdatedAt");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Advisor)
                .HasForeignKey<Advisor>(d => d.Id)
                .HasConstraintName("FK__Advisors__Id__5BE2A6F2");
        });

        modelBuilder.Entity<Assistant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assistants__3213E83FCC772FB1");

            entity.ToTable("Assistants", "App");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");
            entity.Property(e => e.AssignedToConsultant).HasColumnName("AssignedToConsultant");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedAt");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LastName");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Phone");
            entity.Property(e => e.StartDate).HasColumnName("StartDate");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UpdatedAt");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Assistant)
                .HasForeignKey<Assistant>(d => d.Id)
                .HasConstraintName("FK__Assistants__Id__6477ECF3");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customers__3213E83FD08F52F3");

            entity.ToTable("Customers", "App");

            entity.HasIndex(e => e.NitId, "UQ__Customers__1DF2B8DD9565B3E4").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("Address");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CompanyName");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ContactPerson");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CreatedAt");
            entity.Property(e => e.Industry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Industry");
            entity.Property(e => e.NitId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NitId");
            entity.Property(e => e.NumberEmployees).HasColumnName("NumberEmployees");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PhoneNumber");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UpdatedAt");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.Id)
                .HasConstraintName("FK__Customers__Id__571DF1D5");
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

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK__RolePermissions__PermissionId__534D60F1"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__RolePermissions__RoleId__52593CB8"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__RolePermissions__C85A54633606CB73");
                        j.ToTable("RolePermissions", "Auth");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("RoleId");
                        j.IndexerProperty<Guid>("PermissionId").HasColumnName("PermissionId");
                    });
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
            entity.Property(e => e.TypeUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TypeUser");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UpdatedAt");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
