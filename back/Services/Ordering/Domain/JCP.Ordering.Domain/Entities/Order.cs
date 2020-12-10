using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.Common;

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
    }
}