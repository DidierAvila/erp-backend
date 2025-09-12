using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar datos iniciales de Roles
            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Roles",
                columns: new[] { "Id", "Name", "Description", "Status", "CreatedAt" },
                values: new object[,]
                {
                    { Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Super Administrador", "Acceso completo a todas las funcionalidades del sistema", true, DateTime.UtcNow },
                    { Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Administrador", "Gestión completa del sistema con algunas restricciones", true, DateTime.UtcNow },
                    { Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Gerente General", "Supervisión y gestión de todas las áreas operativas", true, DateTime.UtcNow },
                    { Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Gerente Financiero", "Gestión completa del área financiera y contable", true, DateTime.UtcNow },
                    { Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Gerente de Ventas", "Gestión del área de ventas y clientes", true, DateTime.UtcNow },
                    { Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Gerente de Inventario", "Gestión del inventario y almacenes", true, DateTime.UtcNow },
                    { Guid.Parse("12121212-1212-1212-1212-121212121212"), "Contador", "Gestión de transacciones financieras y reportes contables", true, DateTime.UtcNow },
                    { Guid.Parse("13131313-1313-1313-1313-131313131313"), "Vendedor", "Gestión de ventas y atención a clientes", true, DateTime.UtcNow },
                    { Guid.Parse("14141414-1414-1414-1414-141414141414"), "Almacenero", "Gestión de inventario y movimientos de stock", true, DateTime.UtcNow },
                    { Guid.Parse("15151515-1515-1515-1515-151515151515"), "Empleado", "Acceso básico a funcionalidades según su área", true, DateTime.UtcNow }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar datos de Roles
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    Guid.Parse("12121212-1212-1212-1212-121212121212"),
                    Guid.Parse("13131313-1313-1313-1313-131313131313"),
                    Guid.Parse("14141414-1414-1414-1414-141414141414"),
                    Guid.Parse("15151515-1515-1515-1515-151515151515")
                });
        }
    }
}
