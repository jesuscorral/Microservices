using JCP.Ordering.Domain.Entities;
using MediatR;

namespace JCP.Ordering.Domain.DomainEvents
{
    public class OrderCreatedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderCreatedDomainEvent(Order order )
        {
            this.Order = order;
        }
    }
}