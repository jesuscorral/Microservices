using System.Collections.Generic;

namespace JCP.Ordering.API.Features.Orders.GetOrders
{
    public class GetOrdersResponseDTO
    {
        public List<OrderDTO> Orders { get; set; }
    }
}