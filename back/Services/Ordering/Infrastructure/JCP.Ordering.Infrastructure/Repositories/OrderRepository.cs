using System;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<Order> AddAsync(Order entity) {

            var t = entity;

            var e = new Order(Guid.NewGuid(),"kla");

            return e;
        }

        public Task<Order> GetAllAsync() {
            throw new NotImplementedException();
        }
    }
}