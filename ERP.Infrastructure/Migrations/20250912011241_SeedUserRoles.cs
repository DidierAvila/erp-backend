using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert UserRole relationships
            migrationBuilder.InsertData(
                table: "UserRoles",
                schema: "Auth",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    // Super Admin - Super Administrador
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA") },
                    
                    // Super Admin - Super Administrador
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB") },

                    // Juan Pérez (Gerente) - Gerente General
                    { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC") },
                    
                    // María González (Contador) - Contador
                    { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("12121212-1212-1212-1212-121212121212") },
                    
                    // Carlos Rodríguez (Vendedor) - Vendedor
                    { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("13131313-1313-1313-1313-131313131313") },
                    
                    // Ana Martínez (Almacenero) - Almacenero
                    { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("14141414-1414-1414-1414-141414141414") },
                    
                    // Luis Torres (Empleado) - Empleado
                    { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("15151515-1515-1515-1515-151515151515") },
                    
                    // Carmen Silva (Empleado) - Empleado
                    { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("15151515-1515-1515-1515-151515151515") },
                    
                    // Roberto Díaz (Vendedor) - Vendedor
                    { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("13131313-1313-1313-1313-131313131313") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete UserRole relationships
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000002"), new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000003"), new Guid("12121212-1212-1212-1212-121212121212") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000004"), new Guid("13131313-1313-1313-1313-131313131313") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000005"), new Guid("14141414-1414-1414-1414-141414141414") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000006"), new Guid("15151515-1515-1515-1515-151515151515") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000007"), new Guid("15151515-1515-1515-1515-151515151515") });
            
            migrationBuilder.DeleteData(
                table: "UserRoles",
                schema: "Auth",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("10000000-0000-0000-0000-000000000008"), new Guid("13131313-1313-1313-1313-131313131313") });
        }
    }
}
