using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Domain.SeedWork;
using MediatR;
using Microsoft.Azure.Cosmos;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public abstract class CosmosDbRepository<T> : IRepository<T> where T : IDomainEvent
    {
        private readonly IMediator _mediator;
        public abstract string CollectionName { get; }
        private List<IDomainEvent> _domainEvents;
        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = "https://localhost:8081"; // ConfigurationManager.AppSettings["EndPointUri"];

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="; // ConfigurationManager.AppSettings["PrimaryKey"];

        private string databaseId = "db";
        private string containerId = "items";

        public CosmosDbRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void AddDomainEvent(T domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);

        }
        public async Task<bool> SaveEntities()
        {
            // TODO - Extraer la creacion del cliente fuera de aqui
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Name", 400);

            foreach (var domainEvent in _domainEvents) 
                {
                // TODO - Buscar como mejorar para que no sea "dynamic" y serialice el tipo correspondiente.
                var adf = await this.container.CreateItemAsync<dynamic>(domainEvent);
            }
            return true;
        }

        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }
    }
}
