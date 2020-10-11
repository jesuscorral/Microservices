using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.SeedWork;
using MediatR;

namespace JCP.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken);

        void AddDomainEvent(INotification domainEvent);

        void ClearDomainEvents();

    }
}