namespace ERP.Domain.DTOs.App
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? NitId { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? NumberEmployees { get; set; }
        public string? Industry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCustomerDto
    {
        public string CompanyName { get; set; } = null!;
        public string? NitId { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? NumberEmployees { get; set; }
        public string? Industry { get; set; }
    }

    public class UpdateCustomerDto
    {
        public string CompanyName { get; set; } = null!;
        public string? NitId { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? NumberEmployees { get; set; }
        public string? Industry { get; set; }
    }
}
