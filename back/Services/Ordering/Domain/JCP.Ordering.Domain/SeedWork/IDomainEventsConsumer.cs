using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IDomainEventsConsumer
    {
        void Consume(AggregateRoot aggregateRoot, IEnumerable<INotification> domainEvents);
    }
}