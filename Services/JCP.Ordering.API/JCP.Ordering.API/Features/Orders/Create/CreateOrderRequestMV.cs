using System;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderRequestMV: IRequest<CreateOrderResponseMV>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}