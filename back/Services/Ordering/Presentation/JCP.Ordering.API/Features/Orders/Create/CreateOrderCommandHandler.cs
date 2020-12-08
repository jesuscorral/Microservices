using System;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.API.IntegrationEvents;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.IntegrationEvents.Events;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderingIntegrationEventService orderingIntegrationEventService;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                         IOrderingIntegrationEventService orderingIntegrationEventService) 
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            var order = new Order(request.Name, request.Amount, request.BuidlOrderItems(request.OrderItems));
            

            var userId = Guid.NewGuid();
            // Add Integration event to clean the basket
            var orderStartedIntegrationEvent = new OrderCreatedIntegrationEvent(userId.ToString());
            await orderingIntegrationEventService.PublishEventsThroughEventBusAsync(orderStartedIntegrationEvent);

            //var CreateOrderResponse = await _orderRepository.SaveEntities();

            return new CreateOrderCommandResponse
            {
                Id = order.id,
                IsSuccess = true
            };
        }
    }
}