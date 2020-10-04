using System;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.SeedWork;
using MediatR;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreated : IDomainEvent
    {
        public Guid AggregateId { get; }
        public string Name { get; }

        public OrderCreated(Guid aggregateId, string name) {
            AggregateId = aggregateId;
            Name = name;
        }
    }
}