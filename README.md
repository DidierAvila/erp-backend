# ERP Backend System

Sistema ERP (Enterprise Resource Planning) desarrollado con .NET 9 y Entity Framework Core, diseñado para gestionar recursos empresariales de manera integral.

## 🏗️ Arquitectura

El proyecto sigue una arquitectura limpia (Clean Architecture) con separación de responsabilidades:

```
ERP.API/          # Capa de presentación (Controllers, Endpoints)
ERP.Application/  # Lógica de aplicación (Services, DTOs, Mappings)
ERP.Domain/       # Entidades de dominio y reglas de negocio
ERP.Infrastructure/ # Acceso a datos (DbContext, Repositories)
ERP.Test/         # Pruebas unitarias e integración
```

## 🚀 Tecnologías

- **.NET 9**: Framework principal
- **Entity Framework Core**: ORM para acceso a datos
- **AutoMapper**: Mapeo de objetos
- **SQL Server**: Base de datos principal
- **xUnit**: Framework de pruebas
- **FluentValidation**: Validación de datos
- **Swagger/OpenAPI**: Documentación de API

## 📦 Módulos del Sistema

### 🔐 Autenticación y Autorización (Auth)
- **Users**: Gestión de usuarios del sistema
- **UserTypes**: Tipos de usuario (Administrator, Manager, Employee, etc.)
- **Roles**: Roles y permisos del sistema
- **Sessions**: Gestión de sesiones de usuario
- **Permissions**: Sistema de permisos granular

### 💰 Finanzas (Finance)
- **Accounts**: Cuentas contables
- **FinancialTransactions**: Transacciones financieras
- **Invoices**: Facturación

### 📦 Inventario (Inventory)
- **Products**: Gestión de productos
- **StockMovements**: Movimientos de inventario
- **Categories**: Categorización de productos

### 🛒 Compras (Purchases)
- **Suppliers**: Gestión de proveedores
- **PurchaseOrders**: Órdenes de compra

### 💼 Ventas (Sales)
- **SalesOrders**: Órdenes de venta
- **SalesOrderItems**: Ítems de órdenes de venta
- **Customers**: Gestión de clientes

## 🛠️ Instalación

### Prerrequisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/sql-server) o SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

### Configuración

1. **Clonar el repositorio**
   ```bash
   git clone <repository-url>
   cd erp-backend
   ```

2. **Configurar la cadena de conexión**
   
   Editar `appsettings.json` en el proyecto `ERP.API`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=ERP;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

3. **Restaurar paquetes NuGet**
   ```bash
   dotnet restore
   ```

4. **Aplicar migraciones**
   ```bash
   dotnet ef database update --project ERP.Infrastructure --startup-project ERP.API
   ```

5. **Ejecutar el proyecto**
   ```bash
   dotnet run --project ERP.API
   ```

6. **Verificar la instalación**
   
   La aplicación estará disponible en:
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`
   
   Usuarios de prueba disponibles:
   - **admin@erp.com** / **Admin123!** (Administrador completo)
   - **superadmin@erp.com** / **SuperAdmin123!** (Super Administrador)
   - **maria.gonzalez@erp.com** / **Manager123!** (Gerente General)

## 🗄️ Base de Datos

### Migraciones Disponibles

1. **InitialCreate**: Creación inicial de todas las tablas del sistema
2. **SeedUserTypes**: Datos iniciales de tipos de usuario
3. **SeedRoles**: Roles del sistema de autorización
4. **SeedUsers**: Usuarios básicos del sistema
5. **SeedUserRoles**: Asignación de roles a usuarios
6. **SeedPermissions**: 83 permisos granulares del sistema
7. **SeedRolePermissions**: Asignación inicial de permisos por rol
8. **UpdateAdministradorPermissions**: Asignación de todos los permisos al rol Administrador

### Comandos de Migración

```bash
# Crear nueva migración
dotnet ef migrations add <NombreMigracion> --project ERP.Infrastructure --startup-project ERP.API

# Aplicar migraciones
dotnet ef database update --project ERP.Infrastructure --startup-project ERP.API

# Revertir migración
dotnet ef migrations remove --project ERP.Infrastructure --startup-project ERP.API

# Ver historial de migraciones
dotnet ef migrations list --project ERP.Infrastructure --startup-project ERP.API
```

### Esquemas de Base de Datos

- **Auth**: Tablas de autenticación y autorización
- **Finance**: Tablas financieras y contables
- **Inventory**: Tablas de inventario y productos
- **Purchases**: Tablas de compras y proveedores
- **Sales**: Tablas de ventas y clientes

## 🔐 Sistema de Permisos

El sistema implementa un control de acceso basado en roles (RBAC) con permisos granulares:

### Permisos por Módulo
- **Auth**: users.create, users.read, users.update, users.delete, roles.manage, permissions.manage
- **Finance**: accounts.create, accounts.read, accounts.update, accounts.delete, transactions.create, transactions.read, transactions.update, transactions.delete
- **Inventory**: products.create, products.read, products.update, products.delete, stock.create, stock.read, stock.update, stock.delete
- **Purchases**: suppliers.create, suppliers.read, suppliers.update, suppliers.delete, purchase_orders.create, purchase_orders.read, purchase_orders.update, purchase_orders.delete
- **Sales**: customers.create, customers.read, customers.update, customers.delete, sales_orders.create, sales_orders.read, sales_orders.update, sales_orders.delete

### Configuración de Roles
Cada rol tiene permisos específicos asignados según las responsabilidades del usuario:
- Los permisos se almacenan en la tabla `Auth.Permissions`
- Las asignaciones se gestionan en `Auth.RolePermissions`
- El rol **Administrador** tiene acceso completo (83 permisos)

## 🔧 Desarrollo

### Estructura de Proyectos

#### ERP.API
- **Controllers**: Controladores REST API organizados por módulo
- **Extensions**: Extensiones para configuración de servicios
- **Program.cs**: Punto de entrada y configuración de la aplicación

#### ERP.Application
- **Core**: Lógica de negocio por módulo
- **Mappings**: Perfiles de AutoMapper
- **Services**: Servicios de aplicación
- **Helpers**: Utilidades y helpers

#### ERP.Domain
- **Entities**: Entidades de dominio
- **DTOs**: Objetos de transferencia de datos
- **Validators**: Validadores FluentValidation
- **Repositories**: Interfaces de repositorios

#### ERP.Infrastructure
- **DbContexts**: Contexto de Entity Framework
- **Repositories**: Implementaciones de repositorios
- **Migrations**: Migraciones de base de datos
- **Services**: Servicios de infraestructura

### Convenciones de Código

- **Nomenclatura**: PascalCase para clases y métodos, camelCase para variables
- **Organización**: Un archivo por clase/interfaz
- **Namespaces**: Reflejan la estructura de carpetas
- **Async/Await**: Usar para operaciones I/O

### Datos de Prueba

El sistema incluye datos de prueba iniciales:

#### Tipos de Usuario
- Administrator, Manager, Employee
- Customer, Supplier, Accountant
- Warehouse, Sales, Purchasing

#### Usuarios del Sistema
- **admin@erp.com**: Administrador del sistema (Rol: Administrador)
- **superadmin@erp.com**: Super Administrador (Rol: Super Administrador)
- **maria.gonzalez@erp.com**: María González (Rol: Gerente General)
- **carlos.rodriguez@erp.com**: Carlos Rodríguez (Rol: Gerente Financiero)
- **ana.martinez@erp.com**: Ana Martínez (Rol: Contador)

#### Roles y Permisos del Sistema
- **Administrador**: 83 permisos (acceso completo a todos los módulos)
- **Super Administrador**: 10 permisos (gestión de usuarios y roles)
- **Gerente General**: 6 permisos (supervisión general)
- **Gerente Financiero**: 7 permisos (módulo financiero)
- **Gerente de Inventario**: 7 permisos (gestión de inventario)
- **Gerente de Ventas**: 4 permisos (módulo de ventas)
- **Contador**: 4 permisos (contabilidad y finanzas)
- **Almacenero**: 3 permisos (gestión de almacén)
- **Vendedor**: 2 permisos (ventas básicas)
- **Empleado**: 2 permisos (acceso básico)

## 🧪 Pruebas

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar pruebas con cobertura
dotnet test --collect:"XPlat Code Coverage"

# Ejecutar pruebas específicas
dotnet test --filter "Category=Unit"
```

## 📚 API Documentation

La documentación de la API está disponible a través de Swagger UI cuando la aplicación está en modo desarrollo:

```
https://localhost:5001/swagger
```

## 🤝 Contribución

1. Fork el proyecto
2. Crear una rama para la funcionalidad (`git checkout -b feature/nueva-funcionalidad`)
3. Commit los cambios (`git commit -am 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Crear un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 🆘 Soporte

Para reportar bugs o solicitar nuevas funcionalidades, por favor crear un issue en el repositorio.

---

**Desarrollado con ❤️ usando .NET 9**