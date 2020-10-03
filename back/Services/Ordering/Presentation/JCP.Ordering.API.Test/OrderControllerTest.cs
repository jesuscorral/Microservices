using System;
using System.Threading;
using System.Threading.Tasks;
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
        private Mock<IMediator> _mediatorMock;

        public OrderControllerTest() {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Create_Order_Success() {

            // Arrange
            var createrOrderRequestVM = new CreateOrderCommand();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(),  default(CancellationToken)))
                .Returns(Task.FromResult(true));
            var orderController = new OrderController(_mediatorMock.Object);

            // Act
            var result = await orderController.CreateOrder(createrOrderRequestVM) as OkObjectResult;

            // Assert
            Assert.Equal(result.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void Get_Orders_Success() {
            // Arrange
            var getOrdersRequestModel = new GetOrdersQuery();
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetOrdersQuery>(), new CancellationToken()))
                .ReturnsAsync(new GetOrdersResponseDTO());

            var orderController = new OrderController(_mediatorMock.Object);
            // Act

            var result = orderController.GetOrders();

            // Assert
            Assert.Equal((result.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
        }
    }
}