using System.Threading.Tasks;
using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }

        public async Task<int> SaveOrderItemAsync(OrderItem orderItem)
        {
            this.orderDbContext.OrderItems.Add(orderItem);
            return await this.orderDbContext.SaveChangesAsync();
        }
    }
}
