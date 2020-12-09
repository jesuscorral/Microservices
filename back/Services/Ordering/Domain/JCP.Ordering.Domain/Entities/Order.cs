using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.Common;

namespace JCP.Ordering.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get;  private set; }
        public string OrderName { get; private set; }
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }

        public ICollection<OrderOrderItem> OrderOrderItems { get; set; }
    }
}