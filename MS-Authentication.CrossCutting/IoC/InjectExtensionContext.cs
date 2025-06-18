using Microsoft.Extensions.DependencyInjection;
namespace MS_Authentication.CrossCutting.IoC;

public static class InjectExtensionContext
{
    public static IServiceCollection InjectDependency(this IServiceCollection services)
    {
        services.InjectRepository();
        services.InjectService();
        services.AddHttpContextAccessor();
        
        return services;
    }
}
