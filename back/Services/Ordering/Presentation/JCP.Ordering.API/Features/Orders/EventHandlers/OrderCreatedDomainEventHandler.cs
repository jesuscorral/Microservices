using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JCP.Ordering.API.Features.Orders.EventHandlers
{
    public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly ILogger<OrderCreatedDomainEventHandler> Logger;

        public OrderCreatedDomainEventHandler(ILogger<OrderCreatedDomainEventHandler> logger)
        {
            this.Logger = logger;
        }

        public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // Guardar datos en la base de datos de lectura de Cosmos DB
            var domainEvent = notification;

            this.Logger.LogInformation($"CleanArchitecture Domain Event: {domainEvent.GetType().Name}");

            return Task.CompletedTask;
        }
    }
}
