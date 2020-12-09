using System.Collections.Generic;

namespace JCP.Ordering.API.Features.Orders.Queries
{
    public class OrdersVm
    {
        public IList<OrderDTO> Orders { get; set; }
    }
}