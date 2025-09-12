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
       "DefaultConnection": "Server=localhost;Database=ERPDatabase;Trusted_Connection=true;TrustServerCertificate=true;"
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

## 🗄️ Base de Datos

### Migraciones Disponibles

1. **InitialCreate**: Creación inicial de todas las tablas
2. **UpdateEntityConfigurations**: Actualización de configuraciones de entidades
3. **SeedUserTypesData**: Datos iniciales de tipos de usuario
4. **SeedUsersData**: Usuarios básicos del sistema

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
- **admin@erp.com**: Administrador del sistema
- **manager@erp.com**: Gerente general
- **accountant@erp.com**: Contador principal
- **sales@erp.com**: Jefe de ventas
- **purchasing@erp.com**: Jefe de compras
- **warehouse@erp.com**: Supervisor de almacén

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