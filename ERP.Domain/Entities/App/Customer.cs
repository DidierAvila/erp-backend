using System;
using System.Collections.Generic;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Entities.App;

public partial class Customer
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

    public virtual User IdNavigation { get; set; } = null!;
}
