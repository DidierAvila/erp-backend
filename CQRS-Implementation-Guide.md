# Implementación del Patrón CQRS para la Entidad User

## Estructura del Patrón CQRS Implementada

### 1. **Commands (Escritura)**
```
ERP.Application/Core/Auth/Commands/
├── Users/
│   ├── CreateUser.cs     - Comando para crear usuarios (con AutoMapper)
│   ├── UpdateUser.cs     - Comando para actualizar usuarios (con AutoMapper)
│   └── DeleteUser.cs     - Comando para eliminar usuarios
└── Handlers/
    ├── IUserCommandHandler.cs    - Interfaz del handler de comandos
    └── UserCommandHandler.cs     - Implementación del handler de comandos
```

### 2. **Queries (Lectura)**
```
ERP.Application/Core/Auth/Queries/
├── Users/
│   ├── GetUserById.cs    - Query para obtener usuario por ID (con AutoMapper)
│   └── GetAllUsers.cs    - Query para obtener todos los usuarios (con AutoMapper)
└── Handlers/
    ├── IUserQueryHandler.cs      - Interfaz del handler de queries
    └── UserQueryHandler.cs       - Implementación del handler de queries
```

### 3. **DTOs (Data Transfer Objects)**
```
ERP.Domain/DTOs/Auth/UserDto.cs
├── UserDto          - DTO para respuestas
├── CreateUserDto    - DTO para creación
├── UpdateUserDto    - DTO para actualización
└── UserLoginDto     - DTO para login
```

### 4. **AutoMapper Profiles**
```
ERP.Application/Mappings/
├── UserProfile.cs    - Mappings específicos para User
└── AuthProfile.cs    - Mappings para entidades de Auth (Role, Permission, etc.)
```

### 5. **Controlador con Inyección de Dependencias**
```
ERP.API/Controllers/Auth/UsersController.cs
- Implementa los 5 endpoints CRUD
- Usa inyección de dependencias para los handlers
- Manejo de errores y logging
```

### 6. **DbContext Actualizado**
```
ERP.Infrastructure/DbContexts/ErpDbContext.cs
- Configuración de entidades con PascalCase
- Tablas y columnas usando nombres PascalCase
- Relaciones correctamente configuradas
- Propiedades completas para User (Password, Phone, TypeUser)
```

## AutoMapper Implementación

### **Perfiles de Mapping Configurados**

#### UserProfile.cs
```csharp
// Entity to DTO
CreateMap<User, UserDto>()

// DTO to Entity para creación
CreateMap<CreateUserDto, User>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))

// DTO to Entity para actualización
CreateMap<UpdateUserDto, User>()
    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
```

### **Ventajas de AutoMapper**
- ✅ **Reduce código repetitivo**: No más mapeo manual
- ✅ **Consistencia**: Mappings centralizados y reutilizables
- ✅ **Mantenibilidad**: Fácil de actualizar mappings
- ✅ **Performance**: Optimizado para mapeos rápidos
- ✅ **Flexibilidad**: Configuración avanzada de mapeos

### **Uso en Commands y Queries**
```csharp
// En CreateUser
var user = _mapper.Map<User>(createUserDto);
var createdUser = await _userRepository.Create(user, cancellationToken);
return _mapper.Map<UserDto>(createdUser);

// En UpdateUser  
_mapper.Map(updateUserDto, existingUser);
await _userRepository.Update(existingUser, cancellationToken);
return _mapper.Map<UserDto>(existingUser);

// En GetUserById
var user = await _userRepository.GetByID(id, cancellationToken);
return _mapper.Map<UserDto>(user);

// En GetAllUsers
var users = await _userRepository.GetAll(cancellationToken);
return _mapper.Map<IEnumerable<UserDto>>(users);
```

## Operaciones CRUD Implementadas

### **CREATE** - POST `/api/auth/users`
- **Command**: `CreateUser` (usando AutoMapper)
- **Handler**: `UserCommandHandler.CreateUser()`
- **Validaciones**: Email requerido, Password requerido, TypeUser requerido
- **Respuesta**: `201 Created` con el usuario creado

### **READ** - GET `/api/auth/users` y GET `/api/auth/users/{id}`
- **Queries**: `GetAllUsers`, `GetUserById` (usando AutoMapper)
- **Handler**: `UserQueryHandler.GetAllUsers()`, `UserQueryHandler.GetUserById()`
- **Respuestas**: `200 OK` con usuario(s) o `404 Not Found`

### **UPDATE** - PUT `/api/auth/users/{id}`
- **Command**: `UpdateUser` (usando AutoMapper)
- **Handler**: `UserCommandHandler.UpdateUser()`
- **Validaciones**: Usuario debe existir
- **Respuesta**: `200 OK` con el usuario actualizado

### **DELETE** - DELETE `/api/auth/users/{id}`
- **Command**: `DeleteUser`
- **Handler**: `UserCommandHandler.DeleteUser()`
- **Validaciones**: Usuario debe existir
- **Respuesta**: `204 No Content`

## Inyección de Dependencias

### Servicios Registrados en `ExtencionServices.cs`:
```csharp
// AutoMapper
services.AddAutoMapper(typeof(UserProfile), typeof(AuthProfile));

// User Commands
services.AddScoped<CreateUser>();
services.AddScoped<UpdateUser>();
services.AddScoped<DeleteUser>();
services.AddScoped<IUserCommandHandler, UserCommandHandler>();

// User Queries  
services.AddScoped<GetUserById>();
services.AddScoped<GetAllUsers>();
services.AddScoped<IUserQueryHandler, UserQueryHandler>();

// Repositories
services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
```

## Configuración de Base de Datos

### Convención de Nomenclatura
- **Esquemas**: `Auth`, `App` (PascalCase)
- **Tablas**: `Users`, `Roles`, `Permissions`, etc. (PascalCase)
- **Columnas**: `Id`, `Email`, `Password`, `CreatedAt`, etc. (PascalCase)
- **Tablas de unión**: `UserRoles`, `RolePermissions` (PascalCase)

### Entidades Actualizadas en DbContext
```csharp
// Ejemplo para User
entity.ToTable("Users", "Auth");
entity.Property(e => e.Email).HasColumnName("Email");
entity.Property(e => e.Password).HasColumnName("Password");
entity.Property(e => e.Phone).HasColumnName("Phone");
entity.Property(e => e.TypeUser).HasColumnName("TypeUser");
```

## Ventajas de esta Implementación

1. **Separación de Responsabilidades**: Commands y Queries están separados
2. **Mantenibilidad**: Cada operación está en su propia clase
3. **Testabilidad**: Fácil de crear unit tests para cada componente
4. **Escalabilidad**: Fácil agregar nuevas operaciones sin afectar existentes
5. **Inyección de Dependencias**: Facilita el testing y la flexibilidad
6. **Consistencia**: Nomenclatura PascalCase en toda la base de datos
7. **AutoMapper**: Elimina código repetitivo y centraliza mappings

## Patrón para Replicar a Otras Entidades

### Para crear CRUD para una nueva entidad (ej: Product):

1. **Crear Commands**:
   - `CreateProduct.cs` (con AutoMapper y IMapper)
   - `UpdateProduct.cs` (con AutoMapper y IMapper)
   - `DeleteProduct.cs`

2. **Crear Queries**:
   - `GetProductById.cs` (con AutoMapper y IMapper)
   - `GetAllProducts.cs` (con AutoMapper y IMapper)

3. **Crear Handlers**:
   - `IProductCommandHandler.cs`
   - `ProductCommandHandler.cs`
   - `IProductQueryHandler.cs`
   - `ProductQueryHandler.cs`

4. **Crear DTOs**:
   - `ProductDto.cs`
   - `CreateProductDto.cs`
   - `UpdateProductDto.cs`

5. **Crear AutoMapper Profile**:
   - `ProductProfile.cs` con mappings Entity ↔ DTO

6. **Crear Controlador**:
   - `ProductsController.cs`

7. **Registrar en DI**:
   - Agregar todos los servicios en `ExtencionServices.cs`
   - Registrar AutoMapper Profile

8. **Actualizar DbContext**:
   - Configurar entidad con PascalCase
   - Definir relaciones y propiedades

## Estado Actual
✅ Estructura CQRS implementada
✅ CRUD completo para User
✅ AutoMapper integrado y configurado
✅ Mappings automáticos Entity ↔ DTO
✅ Perfiles de AutoMapper (UserProfile, AuthProfile)
✅ Inyección de dependencias configurada
✅ Controlador con manejo de errores
✅ DbContext actualizado con PascalCase
✅ Propiedades completas para User (Password, Phone, TypeUser)
✅ Proyecto compila y ejecuta correctamente
✅ Endpoints listos para testing
✅ Código más limpio y mantenible sin mapeo manual
