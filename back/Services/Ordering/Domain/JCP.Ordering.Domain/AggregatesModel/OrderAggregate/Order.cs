using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Domain.SeedWork;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    // TODO - Crear "Audit.cs" como base de la que hereden todas las entidades con la fecha de creacion
    public class Order : AggregateRoot
    {
        private string _name;
        private readonly DateTime _date;
        private readonly double _amount;
        private readonly List<OrderItem> _orderItems;

        public Order(Guid id, string name) : base(id) {
            _name = name;
            var orderCreatedEvent = new OrderCreatedEvent(id, name);

            AddDomainEvent(orderCreatedEvent);
        }


        protected override void RegisterDomainEventAppliers() {
            RegisterDomainEventApplier<OrderCreatedEvent>(Applier);
        }

        private void Applier(OrderCreatedEvent productCreated) {
            _name = productCreated.Name;
        }
    }
}