using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace JCP.Ordering.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> SaveOrderItemAsync(OrderItem orderItem);
    }
}
