using System;
using System.Net;
using System.Threading.Tasks;
using JCP.Ordering.API.Features.Orders.Commands;
using JCP.Ordering.API.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JCP.Ordering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator) {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateOrderCommandResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command) 
        {
            var response =  await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(OrdersVm))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrders() 
        {
            var response = await _mediator.Send(new GetOrdersQuery());
            return Ok(response);
        }
    }
}