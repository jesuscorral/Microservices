using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Queries
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrdersVm>
    {
        public readonly IOrderRepository orderRepository;

        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OrdersVm> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            // TODO - Utilizar automapper para hacer este mapeo o sacarlo a una clase externa. Ejemplo de mapper en proyecto de CleanArchitecture.
            var orders = await orderRepository.GetOrders();

            // Proyect the order list to OrdersVm to return it.
            var ret = new OrdersVm();
            var ordersDto = new List<OrderDTO>();

            foreach (var o in orders)
            {
                var orderDTO = new OrderDTO
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    Name = o.OrderName
                };
                ordersDto.Add(orderDTO);
            }
            ret.Orders = ordersDto;
            return ret;
        }
    }
}
