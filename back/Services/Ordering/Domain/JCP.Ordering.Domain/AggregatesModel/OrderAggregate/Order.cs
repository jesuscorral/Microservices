using System;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    // TODO - Crear "Audit.cs" como base de la que hereden todas las entidades con la fecha de creacion
    public class Order : Entity
    {
        private string Name;
        private DateTime Date;
        private double Amount;

        // TODO - Añadir List<OrderItem>
    }
}