namespace ERP.Domain.DTOs.Auth
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<PermissionDto>? Permissions { get; set; }
    }

    public class CreateRoleDto
    {
        public string Name { get; set; } = null!;
        public List<Guid>? PermissionIds { get; set; }
    }

    public class UpdateRoleDto
    {
        public string Name { get; set; } = null!;
        public List<Guid>? PermissionIds { get; set; }
    }
}
