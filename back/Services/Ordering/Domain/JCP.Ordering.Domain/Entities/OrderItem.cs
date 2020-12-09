using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.Common;

namespace JCP.Ordering.Domain.Entities
{
    public class OrderItem : AuditableEntity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public OrderItem(Guid id,string name, string description, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
        public ICollection<OrderOrderItem> OrderOrderItems { get; set; }

    }
}