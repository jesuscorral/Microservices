using System;
using MediatR;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreatedEvent : INotification
    {
        public Guid AggregateId { get; }
        public string Name { get; }

        public OrderCreatedEvent(Guid aggregateId, string name) {
            AggregateId = aggregateId;
            Name = name;
        }
    }
}