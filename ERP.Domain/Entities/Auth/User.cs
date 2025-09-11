using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Entities.Auth;

[Table(name: "Users", Schema ="Auth")]
public partial class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Addres { get; set; }
    public required string Email { get; set; }
    public string? Password { get; set; }
    public string? Image { get; set; }
    public string? Phone { get; set; }
    public required Guid UserTypeId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string ExtraData { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = [];

    public virtual ICollection<Session> Sessions { get; set; } = [];

    public virtual ICollection<Role> Roles { get; set; } = [];

    public virtual UserTypes? UserType { get; set; }
}
