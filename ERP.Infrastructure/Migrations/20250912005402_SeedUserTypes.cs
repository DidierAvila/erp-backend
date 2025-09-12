using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar datos iniciales de UserTypes
            migrationBuilder.InsertData(
                schema: "Auth",
                table: "UserTypes",
                columns: new[] { "Id", "Name", "Description", "Status" },
                values: new object[,]
                {
                    { Guid.Parse("11111111-1111-1111-1111-111111111111"), "Administrador", "Usuario con acceso completo al sistema", true },
                    { Guid.Parse("22222222-2222-2222-2222-222222222222"), "Gerente", "Usuario con permisos de gestión y supervisión", true },
                    { Guid.Parse("33333333-3333-3333-3333-333333333333"), "Empleado", "Usuario estándar con permisos básicos", true },
                    { Guid.Parse("44444444-4444-4444-4444-444444444444"), "Contador", "Usuario con acceso a módulos financieros", true },
                    { Guid.Parse("55555555-5555-5555-5555-555555555555"), "Vendedor", "Usuario con acceso a módulos de ventas", true },
                    { Guid.Parse("66666666-6666-6666-6666-666666666666"), "Almacenero", "Usuario con acceso a módulos de inventario", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar datos de UserTypes
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "UserTypes",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Guid.Parse("66666666-6666-6666-6666-666666666666")
                });
        }
    }
}
