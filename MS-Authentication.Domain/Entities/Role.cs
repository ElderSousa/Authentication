using System.ComponentModel.DataAnnotations.Schema;
using MS_Authentication.Domain.Model;

namespace MS_Authentication.Domain.Entities;

public class Role : BaseModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    [NotMapped]
    public bool ValidationRegister { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
