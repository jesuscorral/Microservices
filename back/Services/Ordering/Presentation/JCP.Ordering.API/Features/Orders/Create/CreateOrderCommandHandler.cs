using System;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
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
            var userId = Guid.NewGuid();
           

            //var CreateOrderResponse = await _orderRepository.SaveEntities();

            return new CreateOrderCommandResponse
            {
                Id = Guid.NewGuid(),
                IsSuccess = true
            };
        }
    }
}