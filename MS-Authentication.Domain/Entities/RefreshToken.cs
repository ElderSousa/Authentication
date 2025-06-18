using MS_Authentication.Domain.Model;

namespace MS_Authentication.Domain.Entities;

public class RefreshToken : BaseModel
{
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsRevoked { get; set; } = false;

    public User User { get; set; } = new();
}
