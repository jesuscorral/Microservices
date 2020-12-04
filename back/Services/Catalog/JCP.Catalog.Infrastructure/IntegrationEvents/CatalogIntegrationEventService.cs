using System;
using System.Data.Common;
using System.Threading.Tasks;
using JCP.Catalog.Infrastructure.IntegrationEvents.Interfaces;
using JCP.Catalog.Infrastructure.Repositories;
using JCP.EventBus.Events;
using JCP.EventBus.Events.Interfaces;
using JCP.EventLog;
using JCP.EventLog.Services.Interfacces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JCP.Catalog.Infrastructure.IntegrationEvents
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        private readonly CatalogDbContext catalogDbContext;
        private readonly IEventBus eventBus;
        private readonly IEventLogService eventLogService;
        private readonly ILogger<CatalogIntegrationEventService> logger;
        private readonly Func<DbConnection, IEventLogService> integrationEventLogServiceFactory;

        public CatalogIntegrationEventService(CatalogDbContext catalogDbContext, Func<DbConnection, IEventLogService> integrationEventLogServiceFactory,
                                    IEventBus eventBus,
                                    ILogger<CatalogIntegrationEventService> logger)
        {
            this.catalogDbContext = catalogDbContext ?? throw new ArgumentNullException(nameof(catalogDbContext));
            this.integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.eventLogService = this.integrationEventLogServiceFactory(catalogDbContext.Database.GetDbConnection());
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent @event)
        {
            await ResilientTransaction.CreateNew(catalogDbContext).ExecuteAsync(async () => {
                await catalogDbContext.SaveChangesAsync();
                await eventLogService.SaveEventAsync(@event, catalogDbContext.Database.CurrentTransaction);
            });
        }

        public async Task PublishEventsThroughEventBusAsync(IntegrationEvent @event)
        {
            try
            {
                await eventLogService.MarkEventAsInProgressAsync(@event.Id);
                await eventBus.PublishAsync(@event);
                await eventLogService.MarkEventAsPublishedAsync(@event.Id);
            }
            catch (Exception ex)
            {
                // TODO - Implementar un logger
                logger.LogError(ex, "ERROR publishing integration event: '{IntegrationEventId}'", @event.Id);
                await eventLogService.MarkEventAsFailedAsync(@event.Id);
            }
        }
    }
}
