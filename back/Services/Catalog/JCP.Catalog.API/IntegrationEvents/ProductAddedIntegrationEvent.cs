using System;
using JCP.Catalog.Domain.Model;
using JCP.EventBus.Events;

namespace JCP.Catalog.API.IntegrationEvents
{
    public class ProductAddedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductAddedIntegrationEvent(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }
    }
}
