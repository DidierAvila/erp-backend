# Implementación de Paginación, Ordenamiento y Filtrado - Usuarios

## 📋 **Resumen de la Implementación**

Se ha implementado un sistema completo de paginación, ordenamiento y filtrado para el endpoint de usuarios que incluye:

### **✅ Nuevos DTOs Creados:**
- `PaginationRequestDto` - Parámetros base de paginación y ordenamiento
- `PaginationResponseDto<T>` - Respuesta estándar con metadatos de paginación  
- `UserFilterDto` - Filtros específicos para usuarios
- `UserListResponseDto` - Respuesta optimizada para listados

### **✅ Nuevos Endpoints:**
- `GET /api/auth/users` - Lista paginada con filtros
- `GET /api/auth/users/all` - Lista completa sin paginación (admin)

---

## 🔧 **Uso de los Endpoints**

### **1. Lista Paginada con Filtros (Principal)**
```http
GET /api/auth/users?page=1&pageSize=10&sortBy=name&sortDirection=asc&search=john
```

### **2. Lista Completa (Administrativa)**
```http
GET /api/auth/users/all
```

---

## 📊 **Parámetros de Query Disponibles**

### **Paginación:**
- `page`: Número de página (default: 1, mínimo: 1)
- `pageSize`: Registros por página (default: 10, máximo: 100)

### **Ordenamiento:**
- `sortBy`: Campo por el cual ordenar
  - `name` - Por nombre (default)
  - `email` - Por correo electrónico  
  - `phone` - Por teléfono
  - `createdat` - Por fecha de creación
  - `usertypeid` - Por tipo de usuario
- `sortDirection`: Dirección del ordenamiento
  - `asc` - Ascendente (default)
  - `desc` - Descendente

### **Filtros:**
- `search`: Búsqueda general en name y email
- `name`: Filtro específico por nombre (contiene)
- `email`: Filtro específico por email (contiene) 
- `phone`: Filtro específico por teléfono (contiene)
- `roleId`: Filtro por ID del rol (exacto)
- `userTypeId`: Filtro por ID del tipo de usuario (exacto)
- `createdAfter`: Usuarios creados después de esta fecha
- `createdBefore`: Usuarios creados antes de esta fecha

---

## 📝 **Ejemplos de Uso**

### **Ejemplo 1: Búsqueda Simple**
```http
GET /api/auth/users?search=juan&page=1&pageSize=5
```

### **Ejemplo 2: Filtros Específicos**
```http
GET /api/auth/users?name=María&userTypeId=123e4567-e89b-12d3-a456-426614174000&sortBy=createdat&sortDirection=desc
```

### **Ejemplo 3: Filtro por Rango de Fechas**
```http
GET /api/auth/users?createdAfter=2024-01-01&createdBefore=2024-12-31&page=2&pageSize=20
```

### **Ejemplo 4: Ordenamiento Avanzado**
```http
GET /api/auth/users?sortBy=email&sortDirection=asc&page=1&pageSize=25
```

---

## 📤 **Estructura de Respuesta**

### **Respuesta Paginada:**
```json
{
  "data": [
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Juan Pérez",
      "email": "juan.perez@example.com",
      "phone": "+57 300 123 4567",
      "address": "Calle 123 #45-67",
      "roleName": "Administrator",
      "userTypeName": "Internal User",
      "createdAt": "2024-01-15T10:30:00Z",
      "updatedAt": "2024-01-20T15:45:00Z"
    }
    // ... más usuarios
  ],
  "totalRecords": 150,
  "totalPages": 15,
  "currentPage": 1,
  "pageSize": 10,
  "hasPreviousPage": false,
  "hasNextPage": true,
  "sortBy": "name",
  "sortDirection": "asc"
}
```

### **Respuesta Sin Paginación:**
```json
[
  {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "name": "Juan Pérez",
    "email": "juan.perez@example.com",
    // ... campos completos del UserDto
  }
  // ... todos los usuarios
]
```

---

## ⚠️ **Validaciones Implementadas**

### **Parámetros de Paginación:**
- `page` debe ser >= 1 (si es <= 0, se establece a 1)
- `pageSize` debe estar entre 1 y 100 (se ajusta automáticamente)

### **Ordenamiento:**
- `sortDirection` debe ser 'asc' o 'desc' (default: 'asc')
- `sortBy` inválido usa el ordenamiento por defecto (name)

### **Manejo de Errores:**
- `400 Bad Request` - Parámetros inválidos
- `500 Internal Server Error` - Errores del servidor

---

## 🚀 **Próximos Pasos**

Esta implementación sirve como **template base** para otros controladores. Para implementar en otras entidades:

1. **Copiar los DTOs base** (`PaginationRequestDto`, `PaginationResponseDto`)
2. **Crear filtros específicos** (ej: `RoleFilterDto`, `PermissionFilterDto`)  
3. **Crear respuestas optimizadas** (ej: `RoleListResponseDto`)
4. **Implementar la query** siguiendo el patrón de `GetAllUsersFiltered`
5. **Actualizar el controlador** con los nuevos endpoints

### **Entidades Sugeridas para Próxima Implementación:**
- ✅ Users (Completado)
- 🔲 Roles
- 🔲 Permissions  
- 🔲 Products (Inventory)
- 🔲 Financial Transactions

---

## 🎯 **Beneficios de esta Implementación**

✅ **Performance**: Paginación eficiente para grandes datasets  
✅ **UX**: Ordenamiento y búsqueda intuitivos  
✅ **Escalabilidad**: Patrón reutilizable para todas las entidades  
✅ **Consistencia**: API estándar en toda la aplicación  
✅ **Flexibilidad**: Múltiples opciones de filtrado y ordenamiento
