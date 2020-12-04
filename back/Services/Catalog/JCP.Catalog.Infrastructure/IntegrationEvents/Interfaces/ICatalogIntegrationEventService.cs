using System.Threading.Tasks;
using JCP.EventBus.Events;

namespace JCP.Catalog.Infrastructure.IntegrationEvents.Interfaces
{
    public interface ICatalogIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(IntegrationEvent @event);
        Task AddAndSaveEventAsync(IntegrationEvent @event);
    }
}