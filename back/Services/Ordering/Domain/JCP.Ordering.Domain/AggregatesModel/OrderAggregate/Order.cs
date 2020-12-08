using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string OrderName { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public ICollection<OrderOrderItem> OrderOrderItems { get; set; }

    }
}