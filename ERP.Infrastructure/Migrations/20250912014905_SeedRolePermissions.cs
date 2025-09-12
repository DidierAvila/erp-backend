using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Limpiar datos existentes antes de insertar
            migrationBuilder.Sql("DELETE FROM Auth.RolePermissions");

            // Asignaciones básicas de permisos por rol
            migrationBuilder.InsertData(
                table: "RolePermissions",
                schema: "Auth",
                columns: new[] { "RoleId", "PermissionId" },
                values: new object[,]
                {
                    // Super Administrador - Permisos críticos del sistema
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("697201BE-37F0-41CE-AA77-30D36051F830") }, // system.backup
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("646C1F89-760A-477B-8238-BFD9B7971BED") }, // system.logs
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("8B2C5A4C-6945-4739-A918-057854DC8593") }, // system.maintenance
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("95DC57E2-936C-435D-B6F7-934D6C2065C3") }, // system.restore
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("730A1C6B-CA03-45AC-9DF0-7233AD9459A4") }, // system.settings
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("E03CFD72-CD0A-429A-8FD8-E3B492E66E2F") }, // users.create
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("81ADDF88-C92F-4D4A-90E2-1969E80A4551") }, // users.delete
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("A6E82580-5CEF-48AA-ACAD-EDCAA825ADE6") }, // roles.create
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("AF015994-520B-4E03-ABB0-CF0CF9975675") }, // roles.delete
                    { new Guid("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), new Guid("8AC8E2BE-83AE-4BBF-A599-91DAB83CD1F7") }, // roles.assign_permissions

                    // Administrador - Todos los permisos disponibles (83 permisos)
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("E4FF33D5-DD5C-4D57-8090-B035B7574DDE") }, // accounts.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("99555F1F-0CFC-4DD9-BB29-124DE32B86F7") }, // accounts.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("CE7384E2-604F-494D-8EFE-199A4CAA3114") }, // accounts.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("D876FB7A-0B0E-4CBE-A4CE-C1F8F91E99B4") }, // accounts.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("50271E0D-7B01-44F0-B58F-BCD3E8A0AF85") }, // accounts.view_active
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("530D8180-65B6-489D-8080-CA2DFD2641D1") }, // accounts.view_by_type
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("6E2281C1-E626-4DAF-B5EC-7DA12DEAA6A5") }, // analytics.dashboard
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("76AEB57F-CDF2-40DB-B02E-CC175E7253C0") }, // customers.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("16156551-E75F-441A-B279-97C5419FBC1E") }, // customers.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("4F134EB3-2DC0-4806-8519-41CB7727EFC6") }, // customers.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("8E395540-FDE7-4F2C-A360-62A9ACB08C39") }, // customers.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("C946E620-E8F5-4A0A-9AC8-EB5445C1D1E0") }, // financial_transactions.create
                     { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("50208BAA-E4DD-4704-BAC8-6E7B4F9E9566") }, // financial_transactions.read
                     { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("EB3D1E49-EFF9-49E9-B808-B12CDD4C5205") }, // financial_transactions.update
                     { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("FF025327-B3CE-4F99-8D29-29D9D7DAC617") }, // products.create
                     { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("EDACE24C-695C-4326-A2EB-C368CAC4BDE6") }, // products.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("7900C641-31B8-4B3E-8F18-F25D0F62038B") }, // products.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("7303B2E9-E051-4A34-844A-4F7C5B84447E") }, // products.view_low_stock
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("111B4112-1EF3-4BE9-9F05-BB7A90039090") }, // products.view_stock
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("69D6D6C3-9DB2-42D9-936D-89243B6456E4") }, // purchase_orders.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("B53E2581-D8E2-4542-9BF6-A3F052D87AA6") }, // purchase_orders.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("CB63D269-B89C-4E96-BA45-C75CFAFE5F84") }, // purchase_orders.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("7EFF6F0E-E06D-4297-8440-BFB4C1248387") }, // purchase_orders.receive
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("5640346F-B770-4441-999A-0AF9E8EE01A1") }, // purchase_orders.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("C9FE2949-B248-4C13-9E3E-ABA40A21B1D9") }, // reports.financial
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("A04E82F0-1CB1-4FD3-A3A3-8D945AB90724") }, // reports.inventory
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("80C6834A-7DDF-4232-987D-638196CB3972") }, // reports.purchases
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("2145DBDC-3E3B-4074-B3D6-C1C64E4F8124") }, // reports.sales
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("5C2CB257-2D18-4870-9675-DA43B772A5CB") }, // reports.users
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("8AC8E2BE-83AE-4BBF-A599-91DAB83CD1F7") }, // roles.assign_permissions
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("A6E82580-5CEF-48AA-ACAD-EDCAA825ADE6") }, // roles.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("AF015994-520B-4E03-ABB0-CF0CF9975675") }, // roles.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("F12B05F9-C256-4DCD-BCFA-1B791E4CDC82") }, // roles.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("61731B86-0223-44F5-80BE-EB8E9DD8327E") }, // roles.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("DD53AD6B-D901-4B60-9B5A-3D2119032856") }, // sales_orders.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("0C548E61-2970-459A-A46F-4A065C967150") }, // sales_orders.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("93EA6D20-DA32-4EBE-8F57-A92F56A0900B") }, // sales_orders.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("748658A3-03BB-4335-80A4-6F99CA671DED") }, // sales_orders.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("5F90A9B7-6970-4666-96EA-92235143D622") }, // stock_movements.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("52AB9E82-328F-4715-A715-63F78775F730") }, // stock_movements.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("2008781E-67CB-4796-9DFA-E8243960B7D6") }, // stock_movements.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("748E0374-7ECA-4934-B580-9506DD6F4588") }, // stock_movements.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("E7AAC0FB-3A57-425E-B8E8-AF75D69C87E4") }, // suppliers.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("9022FE6A-2700-49F9-AE01-A2485785AAE3") }, // suppliers.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("9A48BDB8-BD4B-4B8D-9ADE-BFDEB0018344") }, // suppliers.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("47BCD068-1F66-4886-AD50-2E21BEA8CD1F") }, // suppliers.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("469B88EF-3D92-4D4D-90D6-FC4952E7A555") }, // suppliers.view_active
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("24FF41FB-B55D-4FD4-ABCF-0513A51427E5") }, // suppliers.view_by_category
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("00E2EE71-5816-4CE6-AEFD-CDA8B1F10D0F") }, // suppliers.view_by_location
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("73CB2962-AEF2-42A4-8407-CF0869625EDE") }, // suppliers.view_by_name
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("24EF1ACC-9A55-4B86-9868-A7FE2D01CA6F") }, // suppliers.view_by_rating
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("4792F730-B1CA-4AF2-A281-8A97250FFEFB") }, // suppliers.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("B47FAC30-82CB-46DA-85B8-B1046AE6916D") }, // suppliers.view_by_name
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("697201BE-37F0-41CE-AA77-30D36051F830") }, // system.backup
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("646C1F89-760A-477B-8238-BFD9B7971BED") }, // system.logs
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("8B2C5A4C-6945-4739-A918-057854DC8593") }, // system.maintenance
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("95DC57E2-936C-435D-B6F7-934D6C2065C3") }, // system.restore
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("730A1C6B-CA03-45AC-9DF0-7233AD9459A4") }, // system.settings
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("B3A75E58-E3C2-4519-9CAE-AC0653AC0EB3") }, // user_types.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("BC6C8488-06C8-4246-8253-A80AC8B5625D") }, // user_types.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("5449C302-93C8-4670-BA28-0567DBA694BA") }, // user_types.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("030640F8-5B1C-4A44-AC0A-7702A4E63008") }, // user_types.update
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("94737AE8-877A-4D01-99EA-8B9F69154F56") }, // users.change_password
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("E03CFD72-CD0A-429A-8FD8-E3B492E66E2F") }, // users.create
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("81ADDF88-C92F-4D4A-90E2-1969E80A4551") }, // users.delete
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("282DA9B9-BA97-4D38-A267-2339DAEB3957") }, // users.manage_additional_data
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("15358FB2-8C18-4F61-B46B-7505E560041C") }, // users.read
                    { new Guid("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), new Guid("E1D93604-6A4B-4468-AE77-00EA465042A0") }, // users.update

                    // Gerente General - Acceso a reportes y gestión operativa
                    { new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), new Guid("6E2281C1-E626-4DAF-B5EC-7DA12DEAA6A5") }, // analytics.dashboard
                    { new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), new Guid("C9FE2949-B248-4C13-9E3E-ABA40A21B1D9") }, // reports.financial
                    { new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), new Guid("A04E82F0-1CB1-4FD3-A3A3-8D945AB90724") }, // reports.inventory
                    { new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), new Guid("80C6834A-7DDF-4232-987D-638196CB3972") }, // reports.purchases
                    { new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), new Guid("2145DBDC-3E3B-4074-B3D6-C1C64E4F8124") }, // reports.sales
                    { new Guid("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), new Guid("5C2CB257-2D18-4870-9675-DA43B772A5CB") }, // reports.users

                    // Gerente Financiero - Permisos financieros y contables
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("E4FF33D5-DD5C-4D57-8090-B035B7574DDE") }, // accounts.create
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("CE7384E2-604F-494D-8EFE-199A4CAA3114") }, // accounts.read
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("D876FB7A-0B0E-4CBE-A4CE-C1F8F91E99B4") }, // accounts.update
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("C946E620-E8F5-4A0A-9AC8-EB5445C1D1E0") }, // financial_transactions.create
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("50208BAA-E4DD-4704-BAC8-6E7B4F9E9566") }, // financial_transactions.read
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("EB3D1E49-EFF9-49E9-B808-B12CDD4C5205") }, // financial_transactions.update
                    { new Guid("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), new Guid("C9FE2949-B248-4C13-9E3E-ABA40A21B1D9") }, // reports.financial

                    // Gerente de Ventas - Permisos de ventas y clientes
                    { new Guid("EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE"), new Guid("76AEB57F-CDF2-40DB-B02E-CC175E7253C0") }, // customers.create
                    { new Guid("EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE"), new Guid("4F134EB3-2DC0-4806-8519-41CB7727EFC6") }, // customers.read
                    { new Guid("EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE"), new Guid("8E395540-FDE7-4F2C-A360-62A9ACB08C39") }, // customers.update
                    { new Guid("EEEEEEEE-EEEE-EEEE-EEEE-EEEEEEEEEEEE"), new Guid("2145DBDC-3E3B-4074-B3D6-C1C64E4F8124") }, // reports.sales

                    // Gerente de Inventario - Permisos de productos e inventario
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("FF025327-B3CE-4F99-8D29-29D9D7DAC617") }, // products.create
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("EDACE24C-695C-4326-A2EB-C368CAC4BDE6") }, // products.read
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("7900C641-31B8-4B3E-8F18-F25D0F62038B") }, // products.update
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("7303B2E9-E051-4A34-844A-4F7C5B84447E") }, // products.view_low_stock
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("69D6D6C3-9DB2-42D9-936D-89243B6456E4") }, // purchase_orders.create
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("CB63D269-B89C-4E96-BA45-C75CFAFE5F84") }, // purchase_orders.read
                    { new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), new Guid("A04E82F0-1CB1-4FD3-A3A3-8D945AB90724") }, // reports.inventory

                    // Contador - Permisos contables específicos
                    { new Guid("12121212-1212-1212-1212-121212121212"), new Guid("CE7384E2-604F-494D-8EFE-199A4CAA3114") }, // accounts.read
                    { new Guid("12121212-1212-1212-1212-121212121212"), new Guid("50208BAA-E4DD-4704-BAC8-6E7B4F9E9566") }, // financial_transactions.read
                    { new Guid("12121212-1212-1212-1212-121212121212"), new Guid("C946E620-E8F5-4A0A-9AC8-EB5445C1D1E0") }, // financial_transactions.create
                    { new Guid("12121212-1212-1212-1212-121212121212"), new Guid("C9FE2949-B248-4C13-9E3E-ABA40A21B1D9") }, // reports.financial

                    // Vendedor - Permisos básicos de ventas
                    { new Guid("13131313-1313-1313-1313-131313131313"), new Guid("4F134EB3-2DC0-4806-8519-41CB7727EFC6") }, // customers.read
                    { new Guid("13131313-1313-1313-1313-131313131313"), new Guid("76AEB57F-CDF2-40DB-B02E-CC175E7253C0") }, // customers.create

                    // Almacenero - Permisos de inventario básicos
                    { new Guid("14141414-1414-1414-1414-141414141414"), new Guid("EDACE24C-695C-4326-A2EB-C368CAC4BDE6") }, // products.read
                    { new Guid("14141414-1414-1414-1414-141414141414"), new Guid("7303B2E9-E051-4A34-844A-4F7C5B84447E") }, // products.view_low_stock
                    { new Guid("14141414-1414-1414-1414-141414141414"), new Guid("CB63D269-B89C-4E96-BA45-C75CFAFE5F84") }, // purchase_orders.read

                    // Empleado - Permisos básicos de lectura
                    { new Guid("15151515-1515-1515-1515-151515151515"), new Guid("4F134EB3-2DC0-4806-8519-41CB7727EFC6") }, // customers.read
                    { new Guid("15151515-1515-1515-1515-151515151515"), new Guid("EDACE24C-695C-4326-A2EB-C368CAC4BDE6") } // products.read
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar todas las asignaciones de permisos a roles
            migrationBuilder.Sql("DELETE FROM Auth.RolePermissions");
        }
    }
}
