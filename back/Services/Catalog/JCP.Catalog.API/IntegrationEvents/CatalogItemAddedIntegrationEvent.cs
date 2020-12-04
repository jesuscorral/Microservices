using System;
using JCP.Catalog.Domain.Model;
using JCP.EventBus.Events;

namespace JCP.Catalog.API.IntegrationEvents
{
    public class CatalogItemAddedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public CatalogItemAddedIntegrationEvent(CatalogItem catalogItem)
        {
            Id = catalogItem.Id;
            Name = catalogItem.Name;
            Description = catalogItem.Description;
            Price = catalogItem.Price;
        }
    }
}
