namespace ERP.Domain.DTOs.Auth
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public Guid UserTypeId { get; set; }
        public string? Addres { get; set; }
        public Dictionary<string, object>? AdditionalData { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateUserDto
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public Guid UserTypeId { get; set; }
        public string? Addres { get; set; }
        public Dictionary<string, object>? AdditionalData { get; set; }
    }

    public class UpdateUserDto
    {
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public Guid? UserTypeId { get; set; }
        public string? Addres { get; set; }
        public Dictionary<string, object>? AdditionalData { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        // En una implementación real, aquí tendríamos un token de reset
        public string? ResetToken { get; set; }
    }
}
