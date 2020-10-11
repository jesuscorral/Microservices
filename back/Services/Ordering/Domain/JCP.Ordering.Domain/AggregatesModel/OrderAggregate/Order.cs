using System;
using System.Collections.Generic;
using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Domain.SeedWork;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order : IAggregateRoot
    {
        public Guid id;
        public string orderName;
        public DateTime _date;
        public double _amount;
        public List<OrderItem> _orderItems;
        
        public Order(string name, double amount, List<OrderItem> orderItems)
        {
            id = Guid.NewGuid();
            orderName = name;
            _amount = amount;
            _orderItems = orderItems;
            _date = DateTime.UtcNow;
        }

        //public void AddOrder()
        //{
        //    AddDomainEvent(new OrderCreatedEvent(this));
        //}
    }
}