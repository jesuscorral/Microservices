using System.Threading.Tasks;
using JCP.Ordering.Domain.IntegrationEvents.Events;

namespace JCP.Ordering.API.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(IntegrationEvent evt);
    }
}
