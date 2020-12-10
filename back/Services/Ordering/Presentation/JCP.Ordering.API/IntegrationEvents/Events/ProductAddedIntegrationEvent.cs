using System;
using JCP.EventBus.Events;

namespace JCP.Ordering.Api.IntegrationEvents.Events
{
    public class ProductAddedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductAddedIntegrationEvent(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
