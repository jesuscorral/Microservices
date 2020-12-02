using System.Collections.Generic;
using System.Linq;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommand: IRequest<CreateOrderCommandResponse>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        public CreateOrderCommand() { }

        public List<OrderItem> BuidlOrderItems(List<OrderItemDTO> orderItems) {
            var ret = new List<OrderItem>();
            orderItems?.ToList().ForEach(x => {
                ret.Add(new OrderItem { 
                //Discount = x.Discount,
                //ProductName = x.ProductName,
                //UnitPrice = x.UnitPrice,
                //Units = x.Units
                });
            });

            return ret;
        }
    }
}