using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Commands
{
    public class CreateOrderCommand: IRequest<CreateOrderCommandResponse>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}