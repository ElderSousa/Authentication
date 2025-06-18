using MS_Authentication.Domain.Enums;

namespace MS_Authentication.Application.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public bool Active { get; set; }
    public TypeUserRole typeUserRole { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
