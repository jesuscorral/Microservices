using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    // TODO - Crear "Audit.cs" como base de la que hereden todas las entidades con la fecha de creacion
    public class Order : Entity
    {
        public string Name;
        public DateTime Date;
        public double Amount;
        public List<OrderItem> OrderItems;
    }
}