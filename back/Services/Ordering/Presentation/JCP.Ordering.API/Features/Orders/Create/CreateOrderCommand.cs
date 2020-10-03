using System;
using System.Collections.Generic;
using System.Linq;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        public CreateOrderCommand() { }

        public void Bind(Order entity) {
            entity.Name = this.Name;
            entity.Amount = this.Amount;
            entity.Date = this.Date;
            entity.OrderItems = BuidlOrderItems(entity.OrderItems);
        }

        private List<OrderItem> BuidlOrderItems(List<OrderItem> orderItems) {
            var ret = new List<OrderItem>();
            orderItems?.ToList().ForEach(x => {
                ret.Add(new OrderItem { 
                Discount = x.Discount,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Units = x.Units
                });
            });

            return ret;
        }
    }
}