using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace JCP.Ordering.Infrastructure.Repositories.Interfaces
{
    public interface IOrderingRepository
    {
        Task<int> SaveOrderItemAsync(OrderItem orderItem);
    }
}
