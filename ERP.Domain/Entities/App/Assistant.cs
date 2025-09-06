using System;
using System.Collections.Generic;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Entities.App;

public partial class Assistant
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public int? AssignedToConsultant { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
