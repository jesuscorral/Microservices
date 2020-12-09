using System;
using JCP.Ordering.Domain.Common;

namespace JCP.Ordering.Domain.Entities
{
    public class OrderOrderItem : AuditableEntity
    {
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public Guid OrderItemId { get; private set; }
        public OrderItem OrderItem { get; private set; }
    }
}
