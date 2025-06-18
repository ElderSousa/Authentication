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
            return services;
        }
    }
}
