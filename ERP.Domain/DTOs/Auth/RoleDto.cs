namespace ERP.Domain.DTOs.Auth
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<PermissionDto>? Permissions { get; set; }
    }

    public class CreateRoleDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; } = true;
        public List<Guid>? PermissionIds { get; set; }
        // CreatedAt se asignará automáticamente en el servicio
    }

    public class UpdateRoleDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public List<Guid>? PermissionIds { get; set; }
        // UpdatedAt se asignará automáticamente en el servicio
    }

    public class RoleSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int PermissionCount { get; set; }
        public int UserCount { get; set; }
    }

    public class RoleWithDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
        public ICollection<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
