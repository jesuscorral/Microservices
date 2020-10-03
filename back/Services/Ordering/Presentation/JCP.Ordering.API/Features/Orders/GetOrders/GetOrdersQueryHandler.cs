using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersRequestMV, GetOrdersResponseMV>
    {
        public async Task<GetOrdersResponseMV> Handle(GetOrdersRequestMV request, CancellationToken cancellationToken) {
            return new GetOrdersResponseMV() {
                Orders = new List<OrderVM> {
                    new OrderVM {
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
