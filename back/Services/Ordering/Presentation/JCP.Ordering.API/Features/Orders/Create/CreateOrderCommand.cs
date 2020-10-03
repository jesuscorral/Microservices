using System;
using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommand: IRequest<CreateOrderResponseDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}