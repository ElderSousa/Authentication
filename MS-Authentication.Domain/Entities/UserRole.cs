using System.ComponentModel.DataAnnotations.Schema;
using MS_Authentication.Domain.Model;

namespace MS_Authentication.Domain.Entities;

public class UserRole : BaseModel
{ 
    public Guid UserId { get; set; }
    public User User { get; set; } = new();

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = new();

    [NotMapped]
    public bool ValidationRegister { get; set; }

}
