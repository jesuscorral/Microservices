﻿namespace JCP.Ordering.API.Features.Orders.Create
{
    public class OrderItemDTO
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Units { get; set; }
    }
}
