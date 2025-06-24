

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using static MS_Authentication.Application.Requests.UserRequest;

namespace MS_Authentication.API.Controllers;
/// <summary>
/// Controller responsável pela gestão dos usuários.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private Response _response = new();
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Cria o usuário com os dados informados.
    /// </summary>
    /// <param name="userRequest">Objeto com os parâmetros necessários para criar o usuário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response com o status da requisição.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> CreateAsync(CreateUserRequest userRequest, CancellationToken cancellationToken)
    {
        _response = await _userService.CreateAsync(userRequest, cancellationToken);
        return _response.Error ? BadRequest(_response) : Ok(_response);
    }

    /// <summary>
    /// Busca todos os usuários solicitados na requisição
    /// </summary>
    /// <param name="page">Número da página</param>
    /// <param name="pageSize">Quantidade de itens na página</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna uma lista de paginada de usuários.</returns>
    [HttpGet("{page}/{pageSize}")]
    [ProducesResponseType(typeof(Pagination<UserResponse>), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GetByAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var allUsers = await _userService.GetByAllAsync(page, pageSize, cancellationToken);
        return !allUsers.Itens.Any() ? NoContent() : Ok(allUsers);
    }

    /// <summary>
    /// Busca o usuário pertencente ao Id informado.
    /// </summary>
    /// <param name="id">Parâmetro informado na requisição.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna o usuário solicitado na requisição.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Busca o usuário pertencente ao email informado.
    /// </summary>
    /// <param name="email">Parâmetro informado na requisição.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna o usuário solicitado na requisição.</returns>
    [HttpGet("email/{email}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userService.GetByEmailAsync(email, cancellationToken);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Busca todas as roles que pertencem ao usuário
    /// </summary>
    /// <param name="userId">Parâmetro informado na requisição.</param>
    /// <param name="page">Número da página</param>
    /// <param name="pageSize">Quantidade de itens na página</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna uma lista de paginada de roles pertencente ao usuário.</returns>
    [HttpGet("role/{userId}/{page}/{pageSize}")]
    [ProducesResponseType(typeof(Pagination<UserResponse>), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GetByRoleAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var allRoles = await _userService.GetByRolesAsync(userId, page, pageSize, cancellationToken);
        return !allRoles.Itens.Any() ? NoContent() : Ok(allRoles);
    }

    /// <summary>
    /// Atualiza o usuário com os dados informados.
    /// </summary>
    /// <param name="userRequest">Parâmetro informado para atualização do usuário.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response com o status da requisição.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateUserRequest userRequest, CancellationToken cancellationToken)
    {
        _response = await _userService.UpdateAsync(userRequest, cancellationToken);
        return _response.Error ? base.BadRequest(_response) : base.Ok(_response);
    }

    /// <summary>
    /// Deleta o usuário do Id informado.
    /// </summary>
    /// <param name="id">Parâmetro informado para exclusão do usuário</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response com o status da requisição.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _response = await _userService.SoftDeleteAsync(id, cancellationToken);
        return _response.Error ? BadRequest(_response) : Ok(_response);
    }
}