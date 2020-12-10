using System;
using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.API.Features.Orders.Commands
{
    public class CreateOrderCommand: IRequest<CreateOrderCommandResponse>
    {
        public List<Guid> ProductIds { get; set; }
    }
}