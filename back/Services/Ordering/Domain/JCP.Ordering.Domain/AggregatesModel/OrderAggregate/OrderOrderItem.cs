using System;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderOrderItem
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
