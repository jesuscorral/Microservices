using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.DomainEvents;
using MediatR;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : CosmosDbRepository<IDomainEvent>, IOrderRepository
    {
        public override string CollectionName { get; } = "todoItems";

        public OrderRepository(IMediator mediator) : base(mediator) { }
    }
}
