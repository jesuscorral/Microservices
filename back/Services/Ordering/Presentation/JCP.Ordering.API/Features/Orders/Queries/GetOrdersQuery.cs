using MediatR;

namespace JCP.Ordering.API.Features.Orders.Queries
{
    public class GetOrdersQuery : IRequest<OrdersVm>
    {
    }
}
