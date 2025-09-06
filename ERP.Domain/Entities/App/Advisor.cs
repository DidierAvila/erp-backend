using System;
using System.Collections.Generic;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Entities.App;

public partial class Advisor
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Certifications { get; set; }

    public string Specialization { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User IdNavigation { get; set; } = null!;
}
