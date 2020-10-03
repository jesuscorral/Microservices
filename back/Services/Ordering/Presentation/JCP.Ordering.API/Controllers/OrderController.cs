using System;
using System.Net;
using System.Threading.Tasks;
using JCP.Ordering.API.Features.Orders.Create;
using JCP.Ordering.API.Features.Orders.GetOrders;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateOrder([FromBody] CreateOrderRequestMV command) 
        {
            var response = _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrders() 
        {
            var response = _mediator.Send(new GetOrdersRequestMV());
            return Ok(response);
        }
    }
}
