using JCP.Ordering.API.Features.Orders.Create;
using JCP.Ordering.API.IntegrationEvents;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
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

        public static IServiceCollection InjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

            services.AddTransient<IRequestHandler<CreateOrderCommand, bool>, CreateOrderCommandHandler>(); // MediatR dependency injection example

            return services;
        }
    }
}