using System;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderResponseDTO
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
    }
}