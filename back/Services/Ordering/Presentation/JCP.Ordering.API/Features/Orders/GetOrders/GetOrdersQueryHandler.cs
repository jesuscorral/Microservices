using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersResponseDTO>
    {
        public async Task<GetOrdersResponseDTO> Handle(GetOrdersQuery request, CancellationToken cancellationToken) {
            return new GetOrdersResponseDTO() {
                Orders = new List<OrderDTO> {
                    new OrderDTO {
                        Id = new Guid(),
                        Amount = 13,
                        Date = DateTime.UtcNow,
                        Name = "test_01"
                    }
                }
            };
        }
    }
}
