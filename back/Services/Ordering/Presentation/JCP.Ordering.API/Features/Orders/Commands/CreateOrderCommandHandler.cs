using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
    {
        private readonly IOrderRepository orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository) 
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            // TODO - Mover la obtencion de los datos del procuto a Catalog.BFF
            // Get products 
            var productsList = await orderRepository.GetProducts();
            var order = new Order
            {
                OrderItems = new List<OrderItem>()
            };

            if (productsList?.Any() == true)
            {
                var products = productsList.Where(x => request.ProductIds.Contains(x.Id));
                if (products?.Any() == true)
                {
                    foreach (var p in products)
                    {
                        var orderItem = new OrderItem(order.Id, p.Id);
                        order.OrderItems.Add(orderItem);
                    }
                }
            }
           
            await orderRepository.SaveOrder(cancellationToken, order);

            // TODO - Comprobar cuando el saveOrders falle y devolver un false.
            return new CreateOrderCommandResponse
            {
                Id = order.Id,
                IsSuccess = true
            };
        }
    }
}