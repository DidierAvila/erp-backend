namespace ERP.Domain.DTOs.Auth
{
    public class UserTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
    }

    public class CreateUserTypeDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; } = true;
    }

    public class UpdateUserTypeDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
    }

    public class UserTypeSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }
        public int UserCount { get; set; }
    }

    /// <summary>
    /// DTO optimizado para dropdowns/listas desplegables de tipos de usuario (máximo rendimiento)
    /// </summary>
    public class UserTypeDropdownDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
