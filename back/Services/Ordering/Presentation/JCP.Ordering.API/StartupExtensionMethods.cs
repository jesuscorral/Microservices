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
            //this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });

            //this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);

            //this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Name", 400);


            //ItemResponse<Order> wakefieldFamilyResponse = await this.container.CreateItemAsync<Order>(order, new PartitionKey(order.Name));

            //// Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            //Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", wakefieldFamilyResponse.Resource.Id, wakefieldFamilyResponse.RequestCharge);


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