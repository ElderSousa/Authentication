using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS_Authentication.Infrastructure.Data;

namespace MS_Authentication.CrossCutting.IoC;

public static class InjectDependencyExtensionsDataBase
{
    public static IServiceCollection InjectDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AuthDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                mysqlOptions => mysqlOptions.EnableRetryOnFailure()));

        return services;
    }
}
