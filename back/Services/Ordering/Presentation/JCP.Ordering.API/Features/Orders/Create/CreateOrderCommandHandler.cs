using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderRequestMV, CreateOrderResponseMV>
    {
        public async Task<CreateOrderResponseMV> Handle(CreateOrderRequestMV request, CancellationToken cancellationToken) 
        {
            var ret = new CreateOrderResponseMV {
                Id = new Guid(),
                IsSuccess = true
            };
            return ret;

            // TODO - Business logic here
        }
    }
}
