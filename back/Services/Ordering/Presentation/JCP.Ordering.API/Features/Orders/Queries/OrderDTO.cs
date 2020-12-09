using System;

namespace JCP.Ordering.API.Features.Orders.Queries
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}