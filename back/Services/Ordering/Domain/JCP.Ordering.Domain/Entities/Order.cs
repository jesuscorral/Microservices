using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.Common;

namespace JCP.Ordering.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get;  private set; }
        public string OrderName { get; private set; }
        public double Amount { get; private set; }

        public ICollection<OrderOrderItem> OrderOrderItems { get; set; }
         
        // TODO - Revisar la relacion entre las orders y los orderItems - ¿Cambiar orderItem por producto?
        public Order(string orderName, double amount)
        {
            this.OrderName = orderName;
            this.Amount = amount;
            this.Created = DateTime.UtcNow;
        }
    }
}