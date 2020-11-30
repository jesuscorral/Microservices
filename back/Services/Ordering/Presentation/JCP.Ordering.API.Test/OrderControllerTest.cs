using System;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.API.Controllers;
using JCP.Ordering.API.Features.Orders.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace JCP.Ordering.API.Test
{
    public class OrderControllerTest
    {
        private Mock<IMediator> mediatorMock;

        [Fact]
        public async Task OrderController_CreateOrder_Ok()
        {
            // Arrange
            var sut = this.CreateSut();
            var command = new CreateOrderCommand();

            mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), new CancellationToken()))
                                    .Returns(Task.FromResult(new CreateOrderCommandResponse { IsSuccess = true }));

            // Act
            var ret = await sut.CreateOrder(command);

            // Assert
            Assert.IsType<OkObjectResult>(ret);
            Assert.NotNull(ret);
            var result = ret as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode );
        }

        [Fact]
        public async Task OrderController_CreateOrder_BadRequest()
        {
            // Arrange
            var sut = this.CreateSut();
            var command = new CreateOrderCommand();

            mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), new CancellationToken()))
                                    .Returns(Task.FromResult(new CreateOrderCommandResponse { IsSuccess = false}));

            // Act
            var ret = await sut.CreateOrder(command);

            // Assert
            Assert.IsType<BadRequestResult>(ret);
            Assert.NotNull(ret);
        }

        [Fact]
        public async Task OrderController_GetOrders_Ok()
        {
            // Arrange
            var sut = this.CreateSut();

            // Act
            var ret = await sut.GetOrders();

            // Assert
            Assert.IsType<OkObjectResult>(ret);
            Assert.NotNull(ret);
        }

        [Fact]
        public void OrderController_Inherits_ControllerBase()
        {
            Assert.True(typeof(OrderController).IsSubclassOf(typeof(ControllerBase)));
        }

        [Fact]
        public void OrderController_Constructor()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderController(null));
        }

        public OrderController CreateSut()
        {
            mediatorMock = new Mock<IMediator>();

            return new OrderController(mediatorMock.Object);
        }
    }
}
