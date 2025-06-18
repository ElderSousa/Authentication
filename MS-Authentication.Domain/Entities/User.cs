using System.ComponentModel.DataAnnotations.Schema;
using MS_Authentication.Domain.Enums;
using MS_Authentication.Domain.Model;

namespace MS_Authentication.Domain.Entities;

public class User : BaseModel
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool Active { get; set; }
    public TypeUserRole typeUserRole { get; set; }

    [NotMapped]
    public bool ValidationRegister { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
