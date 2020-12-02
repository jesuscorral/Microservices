using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<OrderItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Used fluent API, Data Annotations could be used instead of Fluent API
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }
    }
}