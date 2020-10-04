using System.Collections.Generic;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IDomainEventsConsumer
    {
        void Consume(AggregateRoot aggregateRoot, IEnumerable<IDomainEvent> domainEvents);
    }
}