namespace ERP.Domain.DTOs.Auth
{
    public class UserTypesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
    }

    public class CreateUserTypesDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; } = true;
    }

    public class UpdateUserTypesDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}
