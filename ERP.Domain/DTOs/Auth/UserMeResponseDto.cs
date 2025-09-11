namespace ERP.Domain.DTOs.Auth
{
    /// <summary>
    /// DTO de respuesta estándar para el endpoint /api/auth/me
    /// Implementa el formato híbrido con navegación y permisos agrupados
    /// </summary>
    public class UserMeResponseDto
    {
        public bool Success { get; set; } = true;
        public UserMeDataDto Data { get; set; } = new();
    }

    public class UserMeDataDto
    {
        // Información básica del usuario
        public UserBasicInfoDto User { get; set; } = new();
        
        // Navegación dinámica basada en permisos
        public List<ERP.Domain.DTOs.Navigation.NavigationItemDto> Navigation { get; set; } = new();
        
        // Permisos agrupados por recurso para acciones específicas
        public Dictionary<string, ResourcePermissionsDto> Permissions { get; set; } = new();
    }

    public class UserBasicInfoDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string? Avatar { get; set; }
    }

    public class ResourcePermissionsDto
    {
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Export { get; set; }
        public bool Import { get; set; }
    }
}
