using System;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Order Order { get; }
        public Guid EventId { get; }

        public OrderCreatedEvent(Guid id, Order order)
        {
            EventId = id;
            Order = order;
        }
    }
}