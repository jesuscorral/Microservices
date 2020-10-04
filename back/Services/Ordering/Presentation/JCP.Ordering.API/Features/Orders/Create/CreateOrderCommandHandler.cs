using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.SeedWork;
using JCP.Ordering.Infrastructure.Context;
using MediatR;
using Microsoft.Azure.Cosmos;

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
            var order = new Order(Guid.NewGuid(), "Name");
           
            //order.ConsumeDomainEventChanges(_domainEventsConsumer);

            return true;

            //// TODO - Add validator with the business logic here
        }
    }
}