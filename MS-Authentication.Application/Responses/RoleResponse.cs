namespace MS_Authentication.Application.Responses;

public class RoleResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
