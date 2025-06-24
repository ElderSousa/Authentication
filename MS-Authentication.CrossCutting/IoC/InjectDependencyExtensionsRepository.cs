using Infrastructure.MS_AuthenticationAutorization.Repository;
using Microsoft.Extensions.DependencyInjection;
using MS_Authentication.Domain.Interfaces;
using MS_Authentication.Infrastructure.Repository;

namespace MS_Authentication.CrossCutting.IoC
{
    public static class InjectDependencyExtensionsRepository
    {
        public static IServiceCollection InjectRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepositoy, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
