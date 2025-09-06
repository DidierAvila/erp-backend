using System;
using System.Collections.Generic;
using ERP.Domain.Entities.Auth;

namespace ERP.Domain.Entities;

public partial class Account
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string Type { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string? AccessToken { get; set; }

    public long? ExpiresAt { get; set; }

    public string? IdToken { get; set; }

    public string? Scope { get; set; }

    public string? SessionState { get; set; }

    public string? TokenType { get; set; }

    public virtual User? User { get; set; }
}
