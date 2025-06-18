using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.MapperExtension;
using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;
using MS_Authentication.Domain.Entities;
using MS_Authentication.Domain.Interfaces;
using static MS_Authentication.Application.Requests.UserRequest;

namespace MS_Authentication.Application.Services;

public class UserService : BaseService, IUserService
{
    private Response _response = new();
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    public UserService(IUserRepository userRepository,
        IValidator<User> userValidator,
        IHttpContextAccessor contextAccessor,
        ILogger<UserService> logger) : base(contextAccessor, logger)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<Response> CreateAsync(CreateUserRequest userRequest, CancellationToken cancellationToken)
    {
        try
        {
            var user = userRequest.MapToUser();
            user.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), true);
            user.ValidationRegister = true;

            _response = await ExecuteValidationResponseAsync(_userValidator, user);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            user.PasswordHash = GeneratePasswordHash(user.PasswordHash);

            if (!await _userRepository.CreateAsync(user, cancellationToken))
                throw new InvalidOperationException("Falha ao criar usuário.");

            return ReturnResponseSuccess();        
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao criar usuário com Email: {Email} em CreateAsync", userRequest.Email);
            throw;
        }
    }


    public async Task<Pagination<UserResponse>> GetByAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var allUsers = UserMappingExtension.MapToUserResponse(await _userRepository.GetAllAsync(cancellationToken));

            var pagination = Page(allUsers, page, pageSize);
            if (pagination == null)
                throw new ArgumentException("Falha ao paginar usuários.");

            return pagination;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar todos os usuários em GetAllAsync");
            throw;
        }
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException($"Usuário com Id: {id} não encontrado");

            return UserMappingExtension.MapToUserResponse(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar usuário com Id: {Id} GetIdAsync", id);
            throw;
        }
    }

    public async Task<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException($"Usuário com Email: {email} não encontrado");

            return UserMappingExtension.MapToUserResponse(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar usuário com Email: {Email} GetByEmailAsync", email);
            throw;
        }
    }

    public async Task<Pagination<RoleResponse>> GetByRolesAsync(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var allRoles = RoleMappingExtension.MapToRoleResponse(await _userRepository.GetRolesAsync(userId, cancellationToken));

            var pagination = Page(allRoles, page, pageSize);
            if (pagination == null)
                throw new ArgumentException("Falha ao paginar roles.");

            return pagination;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao buscar role com UserId: {UserId} GetRoleAsync", userId);
            throw;
        }
    }

    public async Task<Response> UpdateAsync(UpdateUserRequest userRequest, CancellationToken cancellationToken)
    {
        try
        {
            var user = userRequest.MapToUser();
            user.ApplyBaseModelFields(GetUserLogged(), DateHourNow(), false);
            user.ValidationRegister = false;

            _response = await ExecuteValidationResponseAsync(_userValidator, user);
            if (_response.Error)
                throw new ArgumentException(_response.Status);

            user.PasswordHash = GeneratePasswordHash(userRequest.PasswordHash);

            if (!await _userRepository.UpdateAsync(user, cancellationToken))
                throw new InvalidOperationException("Falha ao atualizar usuário.");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Falha ao atualizar usuário: {Id}.", userRequest.Id);
            throw;
        }
    }
    public async Task<Response> SoftDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException($"Usuário com Id: {id} não encontrado");

            user.DeletedOn = DateTime.UtcNow;
            user.ModifiedOn = DateTime.UtcNow;

            if (!await _userRepository.SoftDeleteAsync(user, cancellationToken))
                throw new InvalidOperationException($"Falha ao excluir usuário com Id: {id}");

            return ReturnResponseSuccess();
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Falha ao excluir a role com Id: {Id} DeleteAsync", id);
            throw;
        }
    }

    #region METHODS PRIVATE
    public string GeneratePasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    #endregion
}
