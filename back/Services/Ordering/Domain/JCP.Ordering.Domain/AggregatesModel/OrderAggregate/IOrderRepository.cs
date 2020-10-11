using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<IDomainEvent>
    {
    }
}