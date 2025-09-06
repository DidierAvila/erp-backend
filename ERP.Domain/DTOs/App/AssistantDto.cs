namespace ERP.Domain.DTOs.App
{
    public class AssistantDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public int? AssignedToConsultant { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateAssistantDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public int? AssignedToConsultant { get; set; }
        public DateOnly? StartDate { get; set; }
    }

    public class UpdateAssistantDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public int? AssignedToConsultant { get; set; }
        public DateOnly? StartDate { get; set; }
    }
}
