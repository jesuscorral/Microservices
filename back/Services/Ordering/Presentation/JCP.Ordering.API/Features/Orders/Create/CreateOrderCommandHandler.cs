using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponseDTO>
    {
        public async Task<CreateOrderResponseDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            var ret = new CreateOrderResponseDTO {
                Id = new Guid(),
                IsSuccess = true
            };
            return ret;

            // TODO - Business logic here
        }
    }
}
