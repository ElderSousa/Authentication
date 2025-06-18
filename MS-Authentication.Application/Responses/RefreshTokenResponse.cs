namespace MS_Authentication.Application.Responses
{
    public class RefreshTokenResponse
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsRevoked { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
