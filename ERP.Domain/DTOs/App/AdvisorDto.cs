namespace ERP.Domain.DTOs.App
{
    public class AdvisorDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Certifications { get; set; }
        public string Specialization { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateAdvisorDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Certifications { get; set; }
        public string Specialization { get; set; } = null!;
    }

    public class UpdateAdvisorDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Certifications { get; set; }
        public string? Specialization { get; set; }
    }
}
