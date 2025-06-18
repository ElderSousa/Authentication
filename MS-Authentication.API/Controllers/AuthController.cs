using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.Requests;

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
}