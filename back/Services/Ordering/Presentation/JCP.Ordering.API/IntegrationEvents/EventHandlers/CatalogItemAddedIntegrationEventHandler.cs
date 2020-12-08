using System;
using System.Threading.Tasks;
using JCP.EventBus.Events.Interfaces;
using JCP.Ordering.Api.IntegrationEvents.Events;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace JCP.Ordering.Api.IntegrationEvents.EventHandlers
{
    public class CatalogItemAddedIntegrationEventHandler : IIntegrationEventHandler<CatalogItemAddedIntegrationEvent>
    {
        private readonly ILogger<CatalogItemAddedIntegrationEventHandler> _logger;
        private readonly IOrderRepository orderingRepository;

        public CatalogItemAddedIntegrationEventHandler(ILogger<CatalogItemAddedIntegrationEventHandler> logger,
                                                       IOrderRepository orderingRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.orderingRepository = orderingRepository ?? throw new ArgumentNullException(nameof(orderingRepository));
        }

        public async Task HandleAsync(CatalogItemAddedIntegrationEvent @event)
        {
            _logger.LogInformation($"Handling integration event: {@event.Id} - ({@event})");

            var orderItem = new OrderItem
            {
                Id = @event.Id,
                Description = @event.Description,
                Name = @event.Name,
                Price = @event.Price
            };
            await orderingRepository.SaveOrderItemAsync(orderItem);
        }

    }
}
