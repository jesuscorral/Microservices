using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.Domain.SeedWork
{
    public class PersisterDomainEventsConsumer : IDomainEventsConsumer
    {
        private readonly IEventStore _eventStore;

        public PersisterDomainEventsConsumer(IEventStore eventStore) {
            _eventStore = eventStore;
        }

        public void Consume(AggregateRoot aggregateRoot, IEnumerable<INotification> domainEvents) {
            _eventStore.PersistEvents(aggregateRoot.Id.ToString(), domainEvents);
        }
    }
}