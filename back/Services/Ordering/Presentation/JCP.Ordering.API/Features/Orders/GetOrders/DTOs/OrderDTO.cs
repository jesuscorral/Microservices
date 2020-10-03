using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCP.Ordering.API.Features.Orders.GetOrders
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}