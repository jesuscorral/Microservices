using System.Linq;
using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Infrastructure.Repositories;
using MediatR;
using Microsoft.Azure.Cosmos;

namespace JCP.Ordering.Infrastructure
{
    public static class MediatorExtension
    {
        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = "https://localhost:8081"; // ConfigurationManager.AppSettings["EndPointUri"];

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="; // ConfigurationManager.AppSettings["PrimaryKey"];


        // TODO - Hacer generico el ctx para que acepte cualquier repositorio que herede de IRepository
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderRepository ctx)
        {
            var cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync("db");
            Container container = await database.CreateContainerIfNotExistsAsync("items", "/Name", 400);

            var domainEvents = ctx.DomainEvents;
            //var domainEntities = ctx.ChangeTracker
            //    .Entries<AggregateRoot>()
            //    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            //var domainEvents = domainEntities
            //    .SelectMany(x => x.Entity.DomainEvents)
            //    .ToList();

            //domainEntities.ToList()
            //    .ForEach(entity => entity.Entity.ClearDomainEvents());
            var t = (OrderCreatedEvent)domainEvents.FirstOrDefault();

            var andersenFamilyResponse = await container.CreateItemAsync<OrderCreatedEvent>(t);



            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
