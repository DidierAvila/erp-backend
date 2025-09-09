using System.ComponentModel.DataAnnotations;

namespace ERP.Domain.DTOs.Auth
{
    public class RolePermissionDto
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
        public string? RoleName { get; set; }
        public string? PermissionName { get; set; }
    }

    public class CreateRolePermissionDto
    {
        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid PermissionId { get; set; }
    }

    public class AssignPermissionToRoleDto
    {
        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid PermissionId { get; set; }
    }

    public class RoleWithPermissionsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public List<PermissionDto> Permissions { get; set; } = new();
    }
}
