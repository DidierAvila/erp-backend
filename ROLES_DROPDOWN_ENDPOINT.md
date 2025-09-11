# 📝 Endpoint Dropdown de Roles - Documentación

## 🎯 Descripción
Nuevo endpoint optimizado para proporcionar listas desplegables de roles en componentes de frontend. Diseñado específicamente para máximo rendimiento y facilidad de uso en interfaces de usuario.

## 🔧 Endpoint Implementado

### **GET /api/auth/roles/dropdown**

#### **Características:**
- ✅ **Solo roles activos** (`status = true`)
- ✅ **Campos mínimos** (Id + Name) para máximo rendimiento
- ✅ **Ordenamiento alfabético** automático por nombre
- ✅ **Sin paginación** - lista completa
- ✅ **Optimizado para UI** - listo para usar en componentes

#### **Respuesta:**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "name": "Administrador"
  },
  {
    "id": "550e8400-e29b-41d4-a716-446655440002", 
    "name": "Empleado"
  },
  {
    "id": "550e8400-e29b-41d4-a716-446655440003",
    "name": "Manager de Ventas"
  },
  {
    "id": "550e8400-e29b-41d4-a716-446655440004",
    "name": "Supervisor de Inventario"
  }
]
```

## 🚀 Casos de Uso

### **1. Dropdown/Select de Roles**
```javascript
// React/Vue/Angular
const [roleOptions, setRoleOptions] = useState([]);

const loadRoles = async () => {
  const response = await fetch('/api/auth/roles/dropdown');
  const roles = await response.json();
  setRoleOptions(roles); // Ya viene ordenado alfabéticamente
};

// JSX/Template
<select name="roleId">
  <option value="">Seleccionar rol...</option>
  {roleOptions.map(role => (
    <option key={role.id} value={role.id}>
      {role.name}
    </option>
  ))}
</select>
```

### **2. Multiselect de Roles**
```javascript
// Para asignar múltiples roles a un usuario
const assignRoles = async (userId, selectedRoleIds) => {
  await fetch(`/api/auth/users/${userId}/roles`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ roleIds: selectedRoleIds })
  });
};

// Multiselect component
<MultiSelect
  options={roleOptions}
  valueKey="id"
  labelKey="name"
  onChange={setSelectedRoles}
/>
```

### **3. Formulario de Creación de Usuario**
```javascript
// En formulario de crear usuario con roles
const createUser = async (userData) => {
  const payload = {
    name: userData.name,
    email: userData.email,
    password: userData.password,
    userTypeId: userData.userTypeId,
    roleIds: userData.selectedRoles // IDs de roles del dropdown
  };
  
  await fetch('/api/auth/users', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload)
  });
};
```

### **4. Filtros y Búsquedas**
```javascript
// Para filtros en listas de usuarios
<FilterPanel>
  <FormField label="Filtrar por rol">
    <Select 
      options={roleOptions}
      placeholder="Todos los roles"
      onChange={handleRoleFilter}
    />
  </FormField>
</FilterPanel>
```

## 📊 Comparación de Endpoints

| Endpoint | Propósito | Campos | Rendimiento | Uso Principal |
|----------|-----------|--------|-------------|---------------|
| `GET /roles` | Lista completa paginada | Todos + metadatos | Media | Administración de roles |
| `GET /roles/{id}` | Detalle específico | Todos + permisos | Media | Ver detalles del rol |
| **`GET /roles/dropdown`** | **Lista desplegable** | **Solo Id + Name** | **Alta** | **Componentes UI** |
| `GET /roles/simple` | Lista simple | Todos sin paginación | Baja | Casos especiales |

## 🔧 Implementación Técnica

### **DTO Optimizado:**
```csharp
public class RoleDropdownDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
```

### **Query Handler:**
```csharp
public class GetRolesDropdown
{
    // Filtra solo roles activos
    // Ordena alfabéticamente por nombre
    // Mapea a DTO minimalista
}
```

### **Controller:**
```csharp
[HttpGet("dropdown")]
public async Task<ActionResult<IEnumerable<RoleDropdownDto>>> GetRolesDropdown()
```

## ✨ Ventajas del Diseño

### **Para Frontend Developers:**
- **Plug & Play**: Lista lista para usar en cualquier componente
- **Predictible**: Siempre ordenada alfabéticamente  
- **Ligera**: Payload mínimo, carga rápida
- **Estándar**: Formato consistente para todos los dropdowns

### **Para Performance:**
- **Minimal Payload**: Solo 2 campos por registro
- **No Relations**: Sin joins complejos a permisos o usuarios
- **Cacheable**: Respuesta pequeña, ideal para cache del navegador
- **Fast Query**: Consulta simple con filtro y ordenamiento

### **Para UX:**
- **Siempre Ordenado**: No necesita ordenamiento en frontend
- **Solo Activos**: Usuario no ve roles deshabilitados
- **Consistente**: Mismo formato en toda la aplicación

## 🛡️ Manejo de Errores

### **Respuesta de Error:**
```json
{
  "status": 500,
  "message": "Internal server error: [error details]"
}
```

### **Casos de Error:**
- Base de datos no disponible
- Error de mapeo de datos  
- Timeout de consulta

## 🔄 Integración con Sistema Existente

### **Compatible con:**
- ✅ Sistema de creación de usuarios con roles
- ✅ Sistema de asignación de roles a usuarios
- ✅ Sistema de gestión de roles y permisos
- ✅ Todos los endpoints existentes de roles

### **No Conflicta con:**
- ❌ Endpoints de detalle (`GET /roles/{id}`)
- ❌ Endpoints de lista completa (`GET /roles`)
- ❌ Endpoints de gestión (`POST`, `PUT`, `DELETE`)

## 📱 Ejemplos de Frontend

### **React con Fetch API:**
```jsx
const RoleSelect = ({ value, onChange }) => {
  const [roles, setRoles] = useState([]);
  
  useEffect(() => {
    fetch('/api/auth/roles/dropdown')
      .then(res => res.json())
      .then(setRoles);
  }, []);
  
  return (
    <select value={value} onChange={e => onChange(e.target.value)}>
      <option value="">Seleccionar rol...</option>
      {roles.map(role => (
        <option key={role.id} value={role.id}>
          {role.name}
        </option>
      ))}
    </select>
  );
};
```

### **Vue 3 con Composition API:**
```vue
<template>
  <select v-model="selectedRole">
    <option value="">Seleccionar rol...</option>
    <option 
      v-for="role in roles" 
      :key="role.id" 
      :value="role.id"
    >
      {{ role.name }}
    </option>
  </select>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const roles = ref([])
const selectedRole = ref('')

onMounted(async () => {
  const response = await fetch('/api/auth/roles/dropdown')
  roles.value = await response.json()
})
</script>
```

### **Angular con HttpClient:**
```typescript
@Component({
  selector: 'app-role-select',
  template: `
    <select [(ngModel)]="selectedRole">
      <option value="">Seleccionar rol...</option>
      <option 
        *ngFor="let role of roles" 
        [value]="role.id"
      >
        {{ role.name }}
      </option>
    </select>
  `
})
export class RoleSelectComponent implements OnInit {
  roles: RoleDropdownDto[] = [];
  selectedRole: string = '';
  
  constructor(private http: HttpClient) {}
  
  ngOnInit() {
    this.http.get<RoleDropdownDto[]>('/api/auth/roles/dropdown')
      .subscribe(roles => this.roles = roles);
  }
}
```

## 🎯 Conclusión

El endpoint `GET /api/auth/roles/dropdown` es una **adición perfecta** al API que:

- ✅ **Mejora la experiencia del desarrollador frontend**
- ✅ **Optimiza el rendimiento** con payloads mínimos
- ✅ **Sigue patrones estándar** de API design
- ✅ **Se integra perfectamente** con el sistema existente
- ✅ **Proporciona valor inmediato** para componentes UI

**🚀 Listo para usar en producción** con cualquier framework de frontend.
