using System;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using MediatR;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreatedEvent : INotification
    {
        [JsonProperty(PropertyName = "id")]
        public Guid EventId { get; set; }

        public Order Order { get; }

        public OrderCreatedEvent(Guid id, Order order)
        {
            EventId = id;
            Order = order;
        }
    }
}