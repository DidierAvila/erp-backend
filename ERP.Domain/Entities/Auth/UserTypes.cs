using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Entities.Auth
{
    [Table(name: "UserTypes", Schema = "Auth")]
    public partial class UserTypes
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<User> Users { get; set; } = [];
    }
}
