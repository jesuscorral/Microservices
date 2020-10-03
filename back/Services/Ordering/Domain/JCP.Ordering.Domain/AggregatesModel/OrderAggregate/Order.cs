using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    // TODO - Crear "Audit.cs" como base de la que hereden todas las entidades con la fecha de creacion
    public class Order : Entity
    {
        private readonly string _name;
        private readonly DateTime _date;
        private readonly double _amount;
        private readonly List<OrderItem> _orderItems;

    }
}