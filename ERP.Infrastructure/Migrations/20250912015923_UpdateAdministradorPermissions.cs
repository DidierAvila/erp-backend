using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdministradorPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Primero eliminar los permisos actuales del Administrador
            migrationBuilder.Sql(@"
                DELETE FROM Auth.RolePermissions 
                WHERE RoleId = (SELECT Id FROM Auth.Roles WHERE Name = 'Administrador')
            ");

            // Asignar todos los permisos al rol Administrador
            migrationBuilder.Sql(@"
                INSERT INTO Auth.RolePermissions (RoleId, PermissionId)
                SELECT 
                    (SELECT Id FROM Auth.Roles WHERE Name = 'Administrador') as RoleId,
                    Id as PermissionId
                FROM Auth.Permissions
                WHERE Status = 1
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restaurar solo los permisos originales del Administrador
            migrationBuilder.Sql(@"
                DELETE FROM Auth.RolePermissions 
                WHERE RoleId = (SELECT Id FROM Auth.Roles WHERE Name = 'Administrador')
            ");

            // Restaurar permisos originales (los 7 que tenía antes)
            migrationBuilder.InsertData(
                table: "Auth.RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                values: new object[,]
                {
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("6E2281C1-E626-4DAF-B5EC-7DA12DEAA6A5") }, // analytics.dashboard
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("C9FE2949-B248-4C13-9E3E-ABA40A21B1D9") }, // reports.financial
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("A04E82F0-1CB1-4FD3-A3A3-8D945AB90724") }, // reports.inventory
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("80C6834A-7DDF-4232-987D-638196CB3972") }, // reports.purchases
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("2145DBDC-3E3B-4074-B3D6-C1C64E4F8124") }, // reports.sales
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("15358FB2-8C18-4F61-B46B-7505E560041C") }, // users.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("E1D93604-6A4B-4468-AE77-00EA465042A0") }  // users.update
                });
        }
    }
}
