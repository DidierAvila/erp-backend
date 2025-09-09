using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Entities.Auth
{
    [Table(name: "UserTypes", Schema = "Auth")]
    public partial class UserTypes
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
