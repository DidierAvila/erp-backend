namespace ERP.Domain.DTOs.Auth
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreatePermissionDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        // CreatedAt se asignará automáticamente en el servicio
    }

    public class UpdatePermissionDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        // UpdatedAt se asignará automáticamente en el servicio
    }

    public class PermissionSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int RoleCount { get; set; }
    }

    public class PermissionWithRolesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
