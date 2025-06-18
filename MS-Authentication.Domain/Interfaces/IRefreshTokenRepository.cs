using MS_Authentication.Domain.Entities;

public interface IRefreshTokenRepository
{
    Task<bool> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);
    Task<bool> RevokeAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
}
