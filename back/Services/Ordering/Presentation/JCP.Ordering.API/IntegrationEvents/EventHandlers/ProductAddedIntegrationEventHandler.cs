using System;
using System.Threading.Tasks;
using JCP.EventBus.Events.Interfaces;
using JCP.Ordering.Api.IntegrationEvents.Events;
using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace JCP.Ordering.Api.IntegrationEvents.EventHandlers
{
    public class ProductAddedIntegrationEventHandler : IIntegrationEventHandler<ProductAddedIntegrationEvent>
    {
        private readonly ILogger<ProductAddedIntegrationEventHandler> _logger;
        private readonly IOrderRepository orderingRepository;

        public ProductAddedIntegrationEventHandler(ILogger<ProductAddedIntegrationEventHandler> logger,
                                                       IOrderRepository orderingRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.orderingRepository = orderingRepository ?? throw new ArgumentNullException(nameof(orderingRepository));
        }

        public async Task HandleAsync(ProductAddedIntegrationEvent @event)
        {
            _logger.LogInformation($"Handling integration event: {@event.Id} - ({@event})");

            var product = new Product(@event.Id, @event.Description, @event.Name, @event.Price);

            await orderingRepository.SaveProduct(product);
        }

    }
}
