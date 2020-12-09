using System;
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
            var newEntity = new Order(request.Name, request.Amount);
           
            await orderRepository.SaveOrderAsync(cancellationToken, newEntity);

            // TODO - Comprobar cuando el saveOrders falle y devolver un false.
            return new CreateOrderCommandResponse
            {
                Id = newEntity.Id,
                IsSuccess = true
            };
        }
    }
}