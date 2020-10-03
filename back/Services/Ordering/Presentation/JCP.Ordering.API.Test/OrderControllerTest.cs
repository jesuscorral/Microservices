using System;
using System.Threading;
using JCP.Ordering.API.Controllers;
using JCP.Ordering.API.Features.Orders.Create;
using JCP.Ordering.API.Features.Orders.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

// TODO - Add negative tests

namespace JCP.Ordering.API.Test
{
    public class OrderControllerTest
    {
        private Mock<IMediator> _mediator;

        public OrderControllerTest() {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Create_Order_Success() {

            // Arrange
            var createrOrderRequestVM = new CreateOrderCommand();
            _mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), new CancellationToken())).
                ReturnsAsync(new CreateOrderResponseDTO { IsSuccess = true, Id = Guid.NewGuid() });
            var orderController = new OrderController(_mediator.Object);

            // Act
            var result = orderController.CreateOrder(createrOrderRequestVM) as OkObjectResult;

            // Assert
            Assert.Equal(result.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void Get_Orders_Success() {
            // Arrange
            var getOrdersRequestModel = new GetOrdersQuery();
            _mediator.Setup(x => x.Send(It.IsAny<GetOrdersQuery>(), new CancellationToken()))
                .ReturnsAsync(new GetOrdersResponseDTO());

            var orderController = new OrderController(_mediator.Object);
            // Act

            var result = orderController.GetOrders();

            // Assert
            Assert.Equal((result.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);

        }

    }
}
