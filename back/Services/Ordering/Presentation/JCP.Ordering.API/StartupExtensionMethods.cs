using System;
using System.Data.Common;
using System.Reflection;
using JCP.EventBus;
using JCP.EventBus.Events.Interfaces;
using JCP.EventBus.Services;
using JCP.EventBus.Services.Interfaces;
using JCP.EventLog.Services;
using JCP.EventLog.Services.Interfacces;
using JCP.Ordering.Api.IntegrationEvents.EventHandlers;
using JCP.Ordering.Api.IntegrationEvents.Events;
using JCP.Ordering.API.Features.Orders.Create;
using JCP.Ordering.API.IntegrationEvents;
using JCP.Ordering.Infrastructure.Configuration;
using JCP.Ordering.Infrastructure.Configuration.Interface;
using JCP.Ordering.Infrastructure.Repositories;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
            services.AddScoped<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

            services.AddTransient<IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>, CreateOrderCommandHandler>(); // MediatR dependency injection example
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options => 
            {
                options.UseSqlServer(configuration["ConnectionString"],
                                    sqlServerOptionsAction: sqlOptions => {
                                        sqlOptions.MigrationsAssembly(typeof(OrderDbContext).GetTypeInfo().Assembly.GetName().Name);
                                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
            });

            return services;
        }

        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<AzureServiceBusConfiguration>(config.GetSection("AzureServiceBusSettings"));
            services.AddSingleton<IValidateOptions<AzureServiceBusConfiguration>, AzureServiceBusConfigurationValidation>();
            var azureServiceBusConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureServiceBusConfiguration>>().Value;
            services.AddSingleton<IAzureServiceBusConfiguration>(azureServiceBusConfiguration);

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureServiceBusConfiguration = serviceProvider.GetRequiredService<IAzureServiceBusConfiguration>();

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<IIntegrationEventHandler<CatalogItemAddedIntegrationEvent>, CatalogItemAddedIntegrationEventHandler>();

            services.AddSingleton<IServiceBusConnectionManagementService>(sp => 
            {
                var logger = sp.GetRequiredService<ILogger<ServiceBusConnectionManagementService>>();
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(azureServiceBusConfiguration.ConnectionString);
                return new ServiceBusConnectionManagementService(logger, serviceBusConnection);
            });

            services.AddSingleton<IEventBus, AzureServiceBusEventBus>(sp => {
                var serviceBusConnectionManagementService = sp.GetRequiredService<IServiceBusConnectionManagementService>();
                var logger = sp.GetRequiredService<ILogger<AzureServiceBusEventBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var eventBus = new AzureServiceBusEventBus(serviceBusConnectionManagementService, eventBusSubcriptionsManager,
                    serviceProvider, logger, azureServiceBusConfiguration.SubscriptionClientName);
                return eventBus;
            });


            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            serviceProvider = services.BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.SetupAsync().GetAwaiter().GetResult();
            eventBus.SubscribeAsync<CatalogItemAddedIntegrationEvent,
                                    IIntegrationEventHandler<CatalogItemAddedIntegrationEvent>>()
                                    .GetAwaiter().GetResult();

            return services;
        }
    }
}