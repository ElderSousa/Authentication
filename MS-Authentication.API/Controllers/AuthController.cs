using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.Requests;
using MS_Authentication.Application.Responses;

namespace MS_Authentication.API.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Realiza login e gera um token JWT com base nas credenciais informadas.
    /// </summary>
    /// <param name="authRequest">Credenciais de login (email e senha).</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Token JWT gerado se o login for bem-sucedido.</returns>
    [HttpPost]
    public async Task<IActionResult> LoginAsync(AuthRequest authRequest, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(authRequest, cancellationToken);
        return Ok(token);
    }

    /// <summary>
    /// Gera novo access token usando refresh token
    /// </summary>
    /// <param name="refreshToken">Objeto com os parâmetros necessários para gerar o token.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um token.</returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var authResponse = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);
        return authResponse is null ? BadRequest(new Response { Status = "Refresh token inválido", Error = true }) : Ok(authResponse);
    }

    /// <summary>
    /// Realiza logout revogando o refresh token
    /// </summary>
    /// <param name="refreshToken">Objeto com os parâmetros necessários para revogar o token.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response indicando o resultado da requisição.</returns>
    [HttpPost("logout")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        return await _authService.RevokeAsync(refreshToken, cancellationToken) ?
            Ok(new Response { Status = "Success", Error = false }) :
            BadRequest(new Response { Status = "Token inválido ou já revogado.", Error = true });
    }
}