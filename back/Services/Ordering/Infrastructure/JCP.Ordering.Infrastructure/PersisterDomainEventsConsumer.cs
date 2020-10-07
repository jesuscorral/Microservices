using System.Collections.Generic;
using JCP.Ordering.Domain.SeedWork;
using MediatR;

namespace JCP.Ordering.Infrastructure
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