using System;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreatedEvent : BaseEvent
    {
        public Order Order { get; }

        public OrderCreatedEvent(Guid id, Order order)
        {
            EventId = id;
            Order = order;
        }
    }
}