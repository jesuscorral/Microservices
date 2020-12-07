using System;
using System.Data.Common;
using System.Reflection;
using JCP.Catalog.Infrastructure.Configurations;
using JCP.Catalog.Infrastructure.Configurations.Interfaces;
using JCP.Catalog.Infrastructure.IntegrationEvents;
using JCP.Catalog.Infrastructure.IntegrationEvents.Interfaces;
using JCP.Catalog.Infrastructure.Repositories;
using JCP.EventBus;
using JCP.EventBus.Events.Interfaces;
using JCP.EventBus.Services;
using JCP.EventBus.Services.Interfaces;
using JCP.EventLog;
using JCP.EventLog.Services;
using JCP.EventLog.Services.Interfacces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using static JCP.Catalog.Infrastructure.Configurations.AzureServiceBusConfiguration;

namespace JCP.Catalog.API
{
    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
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

        public static IServiceCollection AddDatabasesContexts(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();


            services.AddDbContext<CatalogDbContext>(options => 
            {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                    sqlServerOptionsAction: sqlOptions => {
                                        sqlOptions.MigrationsAssembly(typeof(CatalogDbContext).GetTypeInfo().Assembly.GetName().Name);
                                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                    });
            });

            services.AddDbContext<EventLogContext>(options => {
                options.UseSqlServer(sqlDbConfiguration.ConnectionString,
                                     sqlServerOptionsAction: sqlOptions => {
                                         sqlOptions.MigrationsAssembly(typeof(CatalogDbContext).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                                     });
            });

            return services;
        }

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureServiceBusConfiguration>(configuration.GetSection("AzureServiceBusSettings"));
            services.AddSingleton<IValidateOptions<AzureServiceBusConfiguration>, AzureServiceBusConfigurationValidation>();
            var azureServiceBusConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureServiceBusConfiguration>>().Value;
            services.AddSingleton<IAzureServiceBusConfiguration>(azureServiceBusConfiguration);

            services.Configure<SqlDbDataServiceConfiguration>(configuration.GetSection("SqlDbSettings"));
            services.AddSingleton<IValidateOptions<SqlDbDataServiceConfiguration>, SqlDbDataServiceConfigurationValidation>();
            var sqlDbDataServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<SqlDbDataServiceConfiguration>>().Value;
            services.AddSingleton<ISqlDbDataServiceConfiguration>(sqlDbDataServiceConfiguration);


            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureServiceBusConfiguration = serviceProvider.GetRequiredService<IAzureServiceBusConfiguration>();

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

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
                eventBus.SetupAsync().GetAwaiter().GetResult();

                return eventBus;
            });

            services.AddTransient<Func<DbConnection, IEventLogService>>(
                    sp => (DbConnection connection) => new EventLogService(connection));

            return services;
        }
    }
}