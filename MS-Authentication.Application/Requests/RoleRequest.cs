using System.ComponentModel.DataAnnotations.Schema;

namespace MS_Authentication.Application.Requests;

public class RoleRequest
{
    public class CreateRoleRequest 
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public bool ValidationRegister { get; set; }
    }

    public class UpdateRoleRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public bool ValidationRegister { get; set; }
    }
}
