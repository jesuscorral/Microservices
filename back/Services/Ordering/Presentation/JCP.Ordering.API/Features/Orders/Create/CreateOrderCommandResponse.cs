using System;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandResponse
    {
        public bool IsSuccess { get; set; }
        public Guid Id { get; set; }
    }
}
