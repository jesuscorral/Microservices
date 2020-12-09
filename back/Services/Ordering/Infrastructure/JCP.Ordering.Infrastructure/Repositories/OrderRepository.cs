using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }
        
        public async Task<List<Order>> GetOrders()
        {
            return await this.orderDbContext.Orders.ToListAsync();
        }

        public async Task<int> SaveOrderItemAsync( OrderItem orderItem)
        {
            this.orderDbContext.OrderItems.Add(orderItem);
            return await this.orderDbContext.SaveChangesAsync();
        }

        public async Task<int> SaveOrderAsync(CancellationToken cancellationToken, Order order)
        {
            this.orderDbContext.Orders.Add(order);
            return await this.orderDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
