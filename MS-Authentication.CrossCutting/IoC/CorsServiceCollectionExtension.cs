using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS_Authentication.CrossCutting.IoC;

namespace MS_Authentication.CrossCutting.IoC
{
    public static class CorsServiceCollectionExtension
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("Development", builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("Production", builder =>
                    {
                        builder.WithOrigins("https://meusite.com")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                });
            }

            return services;
        }
    }
}
