namespace ERP.Domain.DTOs.Auth
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? TypeUser { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string TypeUser { get; set; } = null!;
    }

    public class UpdateUserDto
    {
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? TypeUser { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
