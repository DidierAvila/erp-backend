using System.ComponentModel.DataAnnotations.Schema;
using ERP.Domain.Entities.App;

namespace ERP.Domain.Entities.Auth;

[Table(name: "Users", Schema ="Auth")]
public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Image { get; set; }

    public string? Phone { get; set; }

    public string? TypeUser { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual Advisor? Advisor { get; set; }

    public virtual Assistant? Assistant { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
