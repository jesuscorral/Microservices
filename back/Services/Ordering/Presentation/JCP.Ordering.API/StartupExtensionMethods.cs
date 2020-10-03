using JCP.Ordering.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JCP.Ordering.API
{
    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) 
        {
            return services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Microservices-example",
                });
            });
        }

        public static IServiceCollection AddCosmosBDPersistence(this IServiceCollection services, IConfiguration Configuration) 
        {

            services.AddEntityFrameworkCosmos();
            services.AddDbContext<OrderingContext>(options => {
                options.UseCosmos(
                    Configuration["CosmosDB:EndpointUrl"],
                    Configuration["CosmosDb:PrimaryKey"],
                    Configuration["CosmosDb:DbName"]);
            });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices-example");
            });
            return app;
        }
    }
}