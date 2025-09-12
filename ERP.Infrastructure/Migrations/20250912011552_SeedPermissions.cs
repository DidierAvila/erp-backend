using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing permissions to avoid conflicts
            migrationBuilder.Sql("DELETE FROM Auth.Permissions");
            
            // Insert all 83 initial permissions
            migrationBuilder.InsertData(
                table: "Permissions",
                schema: "Auth",
                columns: new[] { "Id", "Name", "Description", "Status" },
                values: new object[,]
                {
                    { new Guid("E4FF33D5-DD5C-4D57-8090-B035B7574DDE"), "accounts.create", "Crear cuentas financieras", true },
                    { new Guid("99555F1F-0CFC-4DD9-BB29-124DE32B86F7"), "accounts.delete", "Eliminar cuentas financieras", true },
                    { new Guid("CE7384E2-604F-494D-8EFE-199A4CAA3114"), "accounts.read", "Consultar cuentas financieras", true },
                    { new Guid("D876FB7A-0B0E-4CBE-A4CE-C1F8F91E99B4"), "accounts.update", "Actualizar cuentas financieras", true },
                    { new Guid("50271E0D-7B01-44F0-B58F-BCD3E8A0AF85"), "accounts.view_active", "Ver cuentas activas", true },
                    { new Guid("530D8180-65B6-489D-8080-CA2DFD2641D1"), "accounts.view_by_type", "Filtrar cuentas por tipo", true },
                    { new Guid("6E2281C1-E626-4DAF-B5EC-7DA12DEAA6A5"), "analytics.dashboard", "Acceder al dashboard de analytics", true },
                    { new Guid("76AEB57F-CDF2-40DB-B02E-CC175E7253C0"), "customers.create", "Crear clientes", true },
                    { new Guid("16156551-E75F-441A-B279-97C5419FBC1E"), "customers.delete", "Eliminar clientes", true },
                    { new Guid("4F134EB3-2DC0-4806-8519-41CB7727EFC6"), "customers.read", "Consultar información de clientes", true },
                    { new Guid("8E395540-FDE7-4F2C-A360-62A9ACB08C39"), "customers.update", "Actualizar información de clientes", true },
                    { new Guid("F120DA59-5476-4A73-9C50-CBC576964DE4"), "financial_accounts.read", "Cuentas bancarias", true },
                    { new Guid("C946E620-E8F5-4A0A-9AC8-EB5445C1D1E0"), "financial_transactions.create", "Crear transacciones financieras", true },
                    { new Guid("AF045469-8438-46BE-8751-7BF914E556C0"), "financial_transactions.delete", "Eliminar transacciones financieras", true },
                    { new Guid("50208BAA-E4DD-4704-BAC8-6E7B4F9E9566"), "financial_transactions.read", "Consultar transacciones financieras", true },
                    { new Guid("EB3D1E49-EFF9-49E9-B808-B12CDD4C5205"), "financial_transactions.update", "Actualizar transacciones financieras", true },
                    { new Guid("F0B9D980-DFEC-4888-830A-DD2BDD5CEC4A"), "financial_transactions.view_by_type", "Filtrar transacciones por tipo", true },
                    { new Guid("4D90BFBA-05F0-4C6C-874A-0A53D9454BAE"), "financial_transactions.view_summary", "Ver resumen financiero", true },
                    { new Guid("7161F392-2AED-4158-904A-CA3E3C675109"), "inventory_locations.create", "Crear ubicaciones de inventario", true },
                    { new Guid("81777076-7A70-42D4-9FF2-AE32706C1D9B"), "inventory_locations.delete", "Eliminar ubicaciones de inventario", true },
                    { new Guid("399D39F0-705E-4B86-95E9-627C65B776A7"), "inventory_locations.read", "Consultar ubicaciones de inventario", true },
                    { new Guid("5CE18AE1-8523-4129-A006-3473C03BED0C"), "inventory_locations.update", "Actualizar ubicaciones de inventario", true },
                    { new Guid("DD6D6093-2DC8-4DFE-8EEA-23C2B7F8223A"), "inventory_locations.view_by_type", "Filtrar ubicaciones por tipo", true },
                    { new Guid("4E2F78F0-6D4C-4E65-9029-EFA4DBB519B1"), "permissions.create", "Crear permisos", true },
                    { new Guid("5412ED73-19A3-487A-BCFD-BEAE36C5A27E"), "permissions.delete", "Eliminar permisos", true },
                    { new Guid("0F4DE161-4AA0-4E78-BBE5-7D083CDDD604"), "permissions.read", "Consultar permisos", true },
                    { new Guid("962F2A3C-5891-4836-B556-DBE287D17876"), "permissions.update", "Actualizar permisos", true },
                    { new Guid("FF025327-B3CE-4F99-8D29-29D9D7DAC617"), "products.create", "Crear productos", true },
                    { new Guid("925748FB-1296-4FA2-BAEA-4E768210ECB8"), "products.delete", "Eliminar productos", true },
                    { new Guid("EDACE24C-695C-4326-A2EB-C368CAC4BDE6"), "products.read", "Consultar productos", true },
                    { new Guid("7900C641-31B8-4B3E-8F18-F25D0F62038B"), "products.update", "Actualizar productos", true },
                    { new Guid("BF16B717-178C-44A1-9365-8ABF43D7C85C"), "products.view_by_sku", "Buscar productos por SKU", true },
                    { new Guid("7303B2E9-E051-4A34-844A-4F7C5B84447E"), "products.view_low_stock", "Ver productos con bajo stock", true },
                    { new Guid("111B4112-1EF3-4BE9-9F05-BB7A90039090"), "purchase_orders.approve", "Aprobar órdenes de compra", true },
                    { new Guid("69D6D6C3-9DB2-42D9-936D-89243B6456E4"), "purchase_orders.create", "Crear órdenes de compra", true },
                    { new Guid("B53E2581-D8E2-4542-9BF6-A3F052D87AA6"), "purchase_orders.delete", "Eliminar órdenes de compra", true },
                    { new Guid("CB63D269-B89C-4E96-BA45-C75CFAFE5F84"), "purchase_orders.read", "Consultar órdenes de compra", true },
                    { new Guid("7EFF6F0E-E06D-4297-8440-BFB4C1248387"), "purchase_orders.receive", "Recibir mercancía de órdenes de compra", true },
                    { new Guid("5640346F-B770-4441-999A-0AF9E8EE01A1"), "purchase_orders.update", "Actualizar órdenes de compra", true },
                    { new Guid("C9FE2949-B248-4C13-9E3E-ABA40A21B1D9"), "reports.financial", "Generar reportes financieros", true },
                    { new Guid("A04E82F0-1CB1-4FD3-A3A3-8D945AB90724"), "reports.inventory", "Generar reportes de inventario", true },
                    { new Guid("80C6834A-7DDF-4232-987D-638196CB3972"), "reports.purchases", "Generar reportes de compras", true },
                    { new Guid("2145DBDC-3E3B-4074-B3D6-C1C64E4F8124"), "reports.sales", "Generar reportes de ventas", true },
                    { new Guid("5C2CB257-2D18-4870-9675-DA43B772A5CB"), "reports.users", "Generar reportes de usuarios", true },
                    { new Guid("8AC8E2BE-83AE-4BBF-A599-91DAB83CD1F7"), "roles.assign_permissions", "Asignar permisos a roles", true },
                    { new Guid("A6E82580-5CEF-48AA-ACAD-EDCAA825ADE6"), "roles.create", "Crear roles", true },
                    { new Guid("AF015994-520B-4E03-ABB0-CF0CF9975675"), "roles.delete", "Eliminar roles", true },
                    { new Guid("F12B05F9-C256-4DCD-BCFA-1B791E4CDC82"), "roles.read", "Consultar roles", true },
                    { new Guid("61731B86-0223-44F5-80BE-EB8E9DD8327E"), "roles.update", "Actualizar roles", true },
                    { new Guid("DD53AD6B-D901-4B60-9B5A-3D2119032856"), "sales_invoices.read", "Consultar las facturas", true },
                    { new Guid("0C548E61-2970-459A-A46F-4A065C967150"), "sales_orders.cancel", "Cancelar órdenes de venta", true },
                    { new Guid("93EA6D20-DA32-4EBE-8F57-A92F56A0900B"), "sales_orders.create", "Crear órdenes de venta", true },
                    { new Guid("748658A3-03BB-4335-80A4-6F99CA671DED"), "sales_orders.delete", "Eliminar órdenes de venta", true },
                    { new Guid("5F90A9B7-6970-4666-96EA-92235143D622"), "sales_orders.process", "Procesar órdenes de venta", true },
                    { new Guid("52AB9E82-328F-4715-A715-63F78775F730"), "sales_orders.read", "Consultar órdenes de venta", true },
                    { new Guid("2008781E-67CB-4796-9DFA-E8243960B7D6"), "sales_orders.update", "Actualizar órdenes de venta", true },
                    { new Guid("748E0374-7ECA-4934-B580-9506DD6F4588"), "sales_orders.view_by_customer", "Ver órdenes por cliente", true },
                    { new Guid("E7AAC0FB-3A57-425E-B8E8-AF75D69C87E4"), "sales_orders.view_by_status", "Filtrar órdenes por estado", true },
                    { new Guid("9022FE6A-2700-49F9-AE01-A2485785AAE3"), "stock_movements.create", "Crear movimientos de stock", true },
                    { new Guid("9A48BDB8-BD4B-4B8D-9ADE-BFDEB0018344"), "stock_movements.delete", "Eliminar movimientos de stock", true },
                    { new Guid("47BCD068-1F66-4886-AD50-2E21BEA8CD1F"), "stock_movements.read", "Consultar movimientos de stock", true },
                    { new Guid("469B88EF-3D92-4D4D-90D6-FC4952E7A555"), "stock_movements.view_by_product", "Ver movimientos por producto", true },
                    { new Guid("24FF41FB-B55D-4FD4-ABCF-0513A51427E5"), "stock_movements.view_by_type", "Filtrar movimientos por tipo", true },
                    { new Guid("00E2EE71-5816-4CE6-AEFD-CDA8B1F10D0F"), "suppliers.create", "Crear proveedores", true },
                    { new Guid("73CB2962-AEF2-42A4-8407-CF0869625EDE"), "suppliers.delete", "Eliminar proveedores", true },
                    { new Guid("24EF1ACC-9A55-4B86-9868-A7FE2D01CA6F"), "suppliers.read", "Consultar proveedores", true },
                    { new Guid("4792F730-B1CA-4AF2-A281-8A97250FFEFB"), "suppliers.update", "Actualizar proveedores", true },
                    { new Guid("B47FAC30-82CB-46DA-85B8-B1046AE6916D"), "suppliers.view_by_name", "Buscar proveedores por nombre", true },
                    { new Guid("697201BE-37F0-41CE-AA77-30D36051F830"), "system.backup", "Realizar respaldos del sistema", true },
                    { new Guid("646C1F89-760A-477B-8238-BFD9B7971BED"), "system.logs", "Ver logs del sistema", true },
                    { new Guid("8B2C5A4C-6945-4739-A918-057854DC8593"), "system.maintenance", "Acceder al modo de mantenimiento", true },
                    { new Guid("95DC57E2-936C-435D-B6F7-934D6C2065C3"), "system.restore", "Restaurar respaldos del sistema", true },
                    { new Guid("730A1C6B-CA03-45AC-9DF0-7233AD9459A4"), "system.settings", "Configurar parámetros del sistema", true },
                    { new Guid("B3A75E58-E3C2-4519-9CAE-AC0653AC0EB3"), "user_types.create", "Crear tipos de usuario", true },
                    { new Guid("BC6C8488-06C8-4246-8253-A80AC8B5625D"), "user_types.delete", "Eliminar tipos de usuario", true },
                    { new Guid("5449C302-93C8-4670-BA28-0567DBA694BA"), "user_types.read", "Consultar tipos de usuario", true },
                    { new Guid("030640F8-5B1C-4A44-AC0A-7702A4E63008"), "user_types.update", "Actualizar tipos de usuario", true },
                    { new Guid("94737AE8-877A-4D01-99EA-8B9F69154F56"), "users.change_password", "Cambiar contraseñas de usuarios", true },
                    { new Guid("E03CFD72-CD0A-429A-8FD8-E3B492E66E2F"), "users.create", "Crear nuevos usuarios", true },
                    { new Guid("81ADDF88-C92F-4D4A-90E2-1969E80A4551"), "users.delete", "Eliminar usuarios", true },
                    { new Guid("282DA9B9-BA97-4D38-A267-2339DAEB3957"), "users.manage_additional_data", "Gestionar datos adicionales de usuarios", true },
                    { new Guid("15358FB2-8C18-4F61-B46B-7505E560041C"), "users.read", "Consultar información de usuarios", true },
                    { new Guid("E1D93604-6A4B-4468-AE77-00EA465042A0"), "users.update", "Actualizar información de usuarios", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all inserted permissions
            var permissionIds = new[]
            {
                // Auth Module
                "A0000000-0000-0000-0000-000000000001", "A0000000-0000-0000-0000-000000000002",
                "A0000000-0000-0000-0000-000000000003", "A0000000-0000-0000-0000-000000000004",
                "A0000000-0000-0000-0000-000000000005", "A0000000-0000-0000-0000-000000000006",
                "A0000000-0000-0000-0000-000000000007", "A0000000-0000-0000-0000-000000000008",
                "A0000000-0000-0000-0000-000000000009", "A000000A-0000-0000-0000-00000000000A",
                // Finance Module
                "F0000000-0000-0000-0000-000000000001", "F0000000-0000-0000-0000-000000000002",
                "F0000000-0000-0000-0000-000000000003", "F0000000-0000-0000-0000-000000000004",
                "F0000000-0000-0000-0000-000000000005", "F0000000-0000-0000-0000-000000000006",
                // Inventory Module
                "10000000-0000-0000-0000-000000000001", "10000000-0000-0000-0000-000000000002",
                "10000000-0000-0000-0000-000000000003", "10000000-0000-0000-0000-000000000004",
                "10000000-0000-0000-0000-000000000005", "10000000-0000-0000-0000-000000000006",
                // Sales Module
                "50000000-0000-0000-0000-000000000001", "50000000-0000-0000-0000-000000000002",
                "50000000-0000-0000-0000-000000000003", "50000000-0000-0000-0000-000000000004",
                "50000000-0000-0000-0000-000000000005",
                // Purchases Module
                "B0000000-0000-0000-0000-000000000001", "B0000000-0000-0000-0000-000000000002",
                "B0000000-0000-0000-0000-000000000003", "B0000000-0000-0000-0000-000000000004",
                "B0000000-0000-0000-0000-000000000005",
                // System Administration
                "E0000000-0000-0000-0000-000000000001", "E0000000-0000-0000-0000-000000000002",
                "E0000000-0000-0000-0000-000000000003"
            };

            foreach (var permissionId in permissionIds)
            {
                migrationBuilder.DeleteData(
                    table: "Permissions",
                    schema: "Auth",
                    keyColumn: "Id",
                    keyValue: new Guid(permissionId)
                );
            }
        }
    }
}
