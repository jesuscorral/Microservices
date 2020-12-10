using System;
using JCP.Ordering.Domain.Common;

namespace JCP.Ordering.Domain.Entities
{
    public class OrderItem : AuditableEntity
    {
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        public OrderItem(Guid orderId, Guid productId)
        {
            OrderId = orderId;
            ProductId = productId;
            Created = DateTime.UtcNow;
        }
    }
}
