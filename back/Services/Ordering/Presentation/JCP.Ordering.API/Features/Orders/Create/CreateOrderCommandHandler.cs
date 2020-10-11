using System;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.DomainEvents;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository) {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            var order = new Order(request.Name, request.Amount, request.BuidlOrderItems(request.OrderItems));

            var t = new OrderCreatedEvent(Guid.NewGuid(), order);
          
            _orderRepository.AddDomainEvent(t);

            return await _orderRepository.SaveEntities();
        }
    }
}