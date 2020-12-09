using System.Threading.Tasks;
using JCP.Ordering.Domain.Entities;

namespace JCP.Ordering.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> SaveOrderItemAsync(OrderItem orderItem);
    }
}
