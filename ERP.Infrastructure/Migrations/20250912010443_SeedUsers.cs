using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar datos iniciales de Users
            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Users",
                columns: new[] { "Id", "Name", "Email", "Password", "UserTypeId", "CreatedAt", "ExtraData" },
                values: new object[,]
                {
                    { Guid.Parse("10000000-0000-0000-0000-000000000001"), "Super Admin", "superadmin@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("11111111-1111-1111-1111-111111111111"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000002"), "Juan Pérez", "juan.perez@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("22222222-2222-2222-2222-222222222222"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000003"), "María González", "maria.gonzalez@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("44444444-4444-4444-4444-444444444444"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000004"), "Carlos Rodríguez", "carlos.rodriguez@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("55555555-5555-5555-5555-555555555555"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000005"), "Ana Martínez", "ana.martinez@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("66666666-6666-6666-6666-666666666666"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000006"), "Luis Torres", "luis.torres@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("33333333-3333-3333-3333-333333333333"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000007"), "Carmen Silva", "carmen.silva@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("33333333-3333-3333-3333-333333333333"), DateTime.UtcNow, "{}" },
                    { Guid.Parse("10000000-0000-0000-0000-000000000008"), "Roberto Díaz", "roberto.diaz@erp.com", "$2a$12$Q1xFDoYrbBLDLZPS.xAzpel/xQGh6kMvtB6ypHhuPDn9UcStuLJ12", Guid.Parse("55555555-5555-5555-5555-555555555555"), DateTime.UtcNow, "{}" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar datos de Users
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    Guid.Parse("10000000-0000-0000-0000-000000000007"),
                    Guid.Parse("10000000-0000-0000-0000-000000000008")
                });
        }
    }
}
