using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using static MS_Authentication.Application.Requests.UserRoleRequest;

namespace MS_Authentication.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiversion}/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private Response _response = new();
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService useRoleService)
        {
            _userRoleService = useRoleService;
        }

        /// <summary>
        /// Cria a userRole com os dados informados.
        /// </summary>
        /// <param name="userRoleRequest">Objeto com os parâmetros necessários para criar a userRole.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna um response com o status da requisição.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 400)]
        public async Task<IActionResult> CreateAsync(CreateUserRoleRequest userRoleRequest, CancellationToken cancellationToken)
        {
            _response = await _userRoleService.CreateAsync(userRoleRequest, cancellationToken);
            return _response.Error ? BadRequest(_response) : Ok(_response);
        }

        /// <summary>
        /// Busca todos as userRoles solicitadas na requisição
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
            var allUserRoles = await _userRoleService.GetAllAsync(page, pageSize, cancellationToken);
            return !allUserRoles.Itens.Any() ? NoContent() : Ok(allUserRoles);
        }

        /// <summary>
        /// Busca a userRole pertencente ao Id informado.
        /// </summary>
        /// <param name="userId">Parâmetro informado na requisição.</param>
        /// <param name="roleId">Parâmetro informado na requisição.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna a role solicitada na requisição.</returns>
        [HttpGet("{userId}/{roleId}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
        {
            try
            {
                var userRole = await _userRoleService.GetIdAsync(userId, roleId, cancellationToken);
                return Ok(userRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza a userRole com os dados informados.
        /// </summary>
        /// <param name="userRoleRequest">Parâmetro informado para atualização da userRole.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna um response com o status da requisição.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateUserRoleRequest userRoleRequest, CancellationToken cancellationToken)
        {
            _response = await _userRoleService.UpdateAsync(userRoleRequest, cancellationToken);
            return _response.Error ? base.BadRequest(_response) : base.Ok(_response);
        }

        /// <summary>
        /// Deleta a userRole do Id informado.
        /// </summary>
        /// <param name="userId">Parâmetro informado na requisição.</param>
        /// <param name="roleId">Parâmetro informado na requisição.</param>
        /// <param name="cancellationToken">Token para cancelamento da operação assíncrona.</param>
        /// <returns>Retorna um response com o status da requisição.</returns>
        [HttpDelete("{userId}/{roleId}")]
        public async Task<IActionResult> SoftDeleteAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
        {
            _response = await _userRoleService.SoftDeleteAsync(userId, roleId, cancellationToken);
            return _response.Error ? BadRequest(_response) : Ok(_response);
        }
    }
}