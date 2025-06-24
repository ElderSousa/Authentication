using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.Requests;
using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Interfaces;

namespace MS_Authentication.Application.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<AuthRequest> _loginValidator;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    private Response _response = new();

    public AuthService(IUserRepository userRepository,
        IValidator<AuthRequest> loginValidator,
        ITokenService tokenService,
        IRefreshTokenService refreshTokenService,
        IHttpContextAccessor contextAccessor,
        ILogger<AuthService> logger) : base(contextAccessor, logger)
    {
        _userRepository = userRepository;
        _loginValidator = loginValidator;
        _tokenService = tokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<AuthResponse> LoginAsync(AuthRequest authRequest, CancellationToken cancellationToken)
    {
        _response = await ExecuteValidationResponseAsync(_loginValidator, authRequest);

        if (_response.Error)
            throw new ArgumentException(_response.Status);

        var user = await _userRepository.GetByEmailAsync(authRequest.Email, cancellationToken);
        if (user is null || !BCrypt.Net.BCrypt.Verify(authRequest.Password, user.PasswordHash))
            throw new KeyNotFoundException("Usuário ou senha inválidos.");

        var roles = await _userRepository.GetByRolesAsync(user.Id, cancellationToken);
        var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = await _refreshTokenService.CreateAsync(user, cancellationToken);

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(refreshToken, cancellationToken);
        if (!isValid)
            throw new UnauthorizedAccessException("Refresh token invalído.");

        var storedToken = await _refreshTokenService.GetByTokenAsync(refreshToken, cancellationToken);

        var user = await _userRepository.GetByIdAsync(storedToken!.UserId, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException("Usuário ou senha inválidos.");

        var roles = await _userRepository.GetByRolesAsync(user.Id, cancellationToken);
        var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = await _refreshTokenService.CreateAsync(user, cancellationToken);

        await _refreshTokenService.RevokeAsync(refreshToken, cancellationToken);

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<bool> RevokeAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _refreshTokenService.RevokeAsync(refreshToken, cancellationToken);
    }
}
