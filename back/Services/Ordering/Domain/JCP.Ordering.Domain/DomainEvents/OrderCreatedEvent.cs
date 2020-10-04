using System;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.SeedWork;
using MediatR;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Guid AggregateId { get; }
        public string Name { get; }

        public OrderCreatedEvent(Guid aggregateId, string name) {
            AggregateId = aggregateId;
            Name = name;
        }
    }
}