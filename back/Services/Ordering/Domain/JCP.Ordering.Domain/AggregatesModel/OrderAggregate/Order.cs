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
        public Order(Guid id, string name)
           : base(id) {
            var productCreated = new OrderCreated(id, name);
            ApplyDomainEvent(productCreated);
        }

        public Order(Guid id, IEnumerable<IDomainEvent> domainEvents)
            : base(id, domainEvents) {
        }

        protected override void RegisterDomainEventAppliers() {
            RegisterDomainEventApplier<OrderCreated>(Applier);
        }

        private void Applier(OrderCreated productCreated) {
            _name = productCreated.Name;
        }
    }
}