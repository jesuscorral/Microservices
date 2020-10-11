using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;
using MediatR;

namespace JCP.Ordering.API.DomainEventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var t = notification;

            // Guardar en la base de datos de lectura.
            throw new NotImplementedException();
        }
    }
}
