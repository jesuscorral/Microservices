using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderingRepository : IOrderingRepository
    {
        private OrderDbContext orderDbContext;

        public OrderingRepository(OrderDbContext orderDbContext)
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
