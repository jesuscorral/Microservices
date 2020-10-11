using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);

        void AddDomainEvent(BaseEvent domainEvent);

        void ClearDomainEvents();

    }
}