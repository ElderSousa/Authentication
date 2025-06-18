namespace MS_Authentication.Application.Responses;

public class UserRoleResponse
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
}
