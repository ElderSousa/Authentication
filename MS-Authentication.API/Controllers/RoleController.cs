

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using static MS_Authentication.Application.Requests.RoleRequest;

namespace MS_Authentication.API.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class RoleController : ControllerBase
{
    private Response _response = new();
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Cria a role com os dados informados.
    /// </summary>
    /// <param name="roleRequest">Objeto com os parâmetros necessários para criar a role.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response com o status da requisição.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Response), 200)]
    [ProducesResponseType(typeof(Response), 400)]
    public async Task<IActionResult> CreateAsync(CreateRoleRequest roleRequest, CancellationToken cancellationToken)
    {
        _response = await _roleService.CreateAsync(roleRequest, cancellationToken);
        return _response.Error ? BadRequest(_response) : Ok(_response);
    }

    /// <summary>
    /// Busca todos as roles solicitadas na requisição
    /// </summary>
    /// <param name="page">Número da página</param>
    /// <param name="pageSize">Quantidade de itens na página</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna uma lista de paginada de roles.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<UserResponse>), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var allRoles = await _roleService.GetAllAsync(page, pageSize, cancellationToken);
        return !allRoles.Itens.Any() ? NoContent() : Ok(allRoles);
    }

    /// <summary>
    /// Busca a role pertencente ao Id informado.
    /// </summary>
    /// <param name="id">Parâmetro informado na requisição.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna a role solicitada na requisição.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _roleService.GetIdAsync(id, cancellationToken);
            return Ok(role);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Atualiza a role com os dados informados.
    /// </summary>
    /// <param name="roleRequest">Parâmetro informado para atualização da role.</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response com o status da requisição.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateRoleRequest roleRequest, CancellationToken cancellationToken)
    {
        _response = await _roleService.UpdateAsync(roleRequest, cancellationToken);
        return _response.Error ? base.BadRequest(_response) : base.Ok(_response);
    }

    /// <summary>
    /// Deleta a role do Id informado.
    /// </summary>
    /// <param name="id">Parâmetro informado para exclusão da role</param>
    /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
    /// <returns>Retorna um response com o status da requisição.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _response = await _roleService.SoftDeleteAsync(id, cancellationToken);
        return _response.Error ? BadRequest(_response) : Ok(_response);
    }
}