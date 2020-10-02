using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JCP.Ordering.API
{
    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services) {
            return services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Microservices-example",
                });
            });
        }
    }
}
