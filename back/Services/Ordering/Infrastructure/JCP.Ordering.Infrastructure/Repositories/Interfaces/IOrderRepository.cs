using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.Entities;

namespace JCP.Ordering.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> SaveOrderItemAsync(OrderItem orderItem);

        Task<List<Order>> GetOrders();

        Task<int> SaveOrderAsync(CancellationToken cancellationToken, Order newEntity);
    }
}