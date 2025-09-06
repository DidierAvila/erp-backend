namespace ERP.Domain.DTOs.Auth
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CreatePermissionDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdatePermissionDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
