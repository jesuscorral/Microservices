using System;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderResponseMV
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
    }
}