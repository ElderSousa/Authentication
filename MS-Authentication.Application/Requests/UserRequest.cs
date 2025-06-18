using MS_Authentication.Domain.Enums;

namespace MS_Authentication.Application.Requests;

public class UserRequest
{
    public class CreateUserRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Active { get; set; }
        public TypeUserRole typeUserRole { get; set; }
    }

    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Active { get; set; }
        public TypeUserRole typeUserRole { get; set; }
    }

}
