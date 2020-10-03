using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Infrastructure.Context;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        public readonly OrderingContext _orderContext;

        public CreateOrderCommandHandler(OrderingContext orderingContext) 
        {
            _orderContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken) 
        {
            //var orderItems = new List<OrderItem>();
            //if (request != null) {
            //    request.OrderItems.ToList().ForEach(x => {
            //        orderItems.Add(new OrderItem(x.ProductName, x.UnitPrice, x.Discount, x.Units));
            //    });
            //}
            //var order = new Order(request.Name, request.Date, request.Amount, orderItems);
            //order.Id = Guid.NewGuid();
            // TODO - Cambiar para utilizar el cancellationToken
            var order = new Order();
            request.Bind(order);
            await _orderContext.Orders.AddAsync(order);
            await _orderContext.SaveChangesAsync();

            
            // TODO - cAMBIAR POR EL UNIT OF WORK
            return true;

            // TODO - Add validator with the business logic here
        }

        
    }
}
