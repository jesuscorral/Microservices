using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.Common;
using JCP.Ordering.Domain.DomainEvents;

namespace JCP.Ordering.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get;  private set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            this.Created = DateTime.UtcNow;
        }

        public Order(List<OrderItem> orderItems)
        {
            this.OrderItems = orderItems;
        }

        public void SendDomainEvent()
        {
            AddDomainEvent(new OrderCreatedDomainEvent(this));
        }
    }
}