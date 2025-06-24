using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MS_Authentication.Application.Interfaces;
using MS_Authentication.Application.Services;
using MS_Authentication.Application.Validation;

namespace MS_Authentication.CrossCutting.IoC;

public static class InjectDependencyExtensionsService
{
    public static IServiceCollection InjectService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<UserRoleService, UserRoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddValidatorsFromAssemblyContaining<UserValidation>();
        return services;
    }
}
