using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;

namespace MS_Authentication.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> CreateAsync(User user, CancellationToken cancellationToken);
        Task<RefreshTokenResponse?> GetByTokenAsync(string token, CancellationToken cancellationToken);
        Task<bool> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken);
        Task<bool> RevokeAsync(string token, CancellationToken cancellationToken);
    }
}
