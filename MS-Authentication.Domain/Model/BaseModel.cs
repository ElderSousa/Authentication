namespace MS_Authentication.Domain.Model;

public class BaseModel
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public void ApplyBaseModelFields(Guid userId, DateTime dateTime, bool register)
    {
        if (register)
        {
            Id = Guid.NewGuid();
            CreatedBy = userId;
            CreatedOn = dateTime;
        }
        else
        {
            ModifiedBy = userId;
            ModifiedOn = dateTime;
        }

    }
}
