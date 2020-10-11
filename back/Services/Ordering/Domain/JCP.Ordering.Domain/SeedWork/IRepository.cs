using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IRepository<T> where T : IDomainEvent
    {
        string CollectionName { get; }

        void AddDomainEvent(T domainEvent);

        Task<bool> SaveEntities();

        void ClearDomainEvents();
    }
}
