using System;
using System.Collections.Generic;

namespace JCP.Ordering.API.Features.Orders.GetOrders
{
    public class GetOrdersResponseMV
    {
        public List<OrderVM> Orders { get; set; }
    }

    public class OrderVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}