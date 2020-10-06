using System.Threading.Tasks;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<T> GetAllAsync();
        Task<T> AddAsync(T entity);
    }
}
