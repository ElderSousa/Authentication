using MS_Authentication.Application.Requests;
using MS_Authentication.Application.Responses;

namespace MS_Authentication.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(AuthRequest authRequest, CancellationToken cancellationToken);
    Task<bool> RevokeAsync(string refreshToken, CancellationToken cancellationToken);
    Task<AuthResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
