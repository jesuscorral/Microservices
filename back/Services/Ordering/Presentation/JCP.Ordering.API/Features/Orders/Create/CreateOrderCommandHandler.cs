using System;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IDomainEventsConsumer _domainEventsConsumer;

        public CreateOrderCommandHandler(IDomainEventsConsumer domainEventsConsumer) {
            _domainEventsConsumer = domainEventsConsumer;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            var newGuid = Guid.NewGuid();
            var order = new Order(Guid.NewGuid(), "Name");
            order.ConsumeDomainEventChanges(_domainEventsConsumer);
            //var ret = await _orderRepository.AddAsync(order);

            // TODO: comprobar que es lo que devuelve el "ret"
            return true;

            //// TODO - Add validator with the business logic here
        }
    }
}