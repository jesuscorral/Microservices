using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.Entities;

namespace JCP.Ordering.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> SaveProduct(Product product);

        Task<List<Order>> GetOrders();

        Task<List<Product>> GetProducts();

        Task<int> SaveOrder(CancellationToken cancellationToken, Order order);
    }
}