using System;
using System.Collections.Generic;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<OrderOrderItem> OrderOrderItems { get; set; }

    }
}