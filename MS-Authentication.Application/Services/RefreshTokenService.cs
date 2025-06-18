using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.MapperExtension;
using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;
using System.Security.Cryptography;

namespace MS_Authentication.Application.Services;

public class RefreshTokenService : BaseService, IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IValidator<User> _userValidator;

    private Response _response = new();

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
        IValidator<User> userValidator,
        IHttpContextAccessor contextAccessor,
        ILogger<RefreshTokenService> logger) : base(contextAccessor, logger)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userValidator = userValidator;
    }

    public async Task<string> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            Token = GenerateToken(),
            UserId = user.Id,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        _response = await ExecuteValidationResponseAsync(_userValidator, user);
        if (_response.Error)
            throw new ArgumentException(_response.Status);
            
        refreshToken.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), true);

        if (!await _refreshTokenRepository.CreateAsync(refreshToken, cancellationToken))
            throw new InvalidOperationException("Falha ao criar RefreshToken.");

        return refreshToken.Token;
    }

    public async Task<RefreshTokenResponse?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(token, cancellationToken);
        if (storedToken is null)
            throw new KeyNotFoundException($"Token: {token} não encontrado em nossa base de dados");

        return RefreshTokenMapping.MapToRefreshTokeResponse(storedToken);
    }

    public async Task<bool> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(token, cancellationToken);
        if (storedToken == null)
            throw new KeyNotFoundException($"Toke: {token} não encontrado em nossa base de dados");

        return storedToken is { IsRevoked: false } && storedToken.ExpirationDate > DateTime.UtcNow;
    }

    public async Task<bool> RevokeAsync(string token, CancellationToken cancellationToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(token, cancellationToken);
        if (storedToken == null)
            throw new KeyNotFoundException($"Toke: {token} não encontrado em nossa base de dados");

        storedToken.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), false);
        storedToken.IsRevoked = true;

        if (!await _refreshTokenRepository.RevokeAsync(storedToken, cancellationToken))
            throw new InvalidOperationException("Falha ao revogar token");

        return true;
    }

    #region METHODS PRIVATE
    private static string GenerateToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    #endregion
}
