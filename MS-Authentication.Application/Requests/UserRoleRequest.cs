using System.ComponentModel.DataAnnotations.Schema;

namespace MS_Authentication.Application.Requests;

public class UserRoleRequest
{
    public class CreateUserRoleRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [NotMapped]
        public bool ValidationRegister { get; set; }
    }

    public class UpdateUserRoleRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [NotMapped]
        public bool ValidationRegister { get; set; }
    }
}
