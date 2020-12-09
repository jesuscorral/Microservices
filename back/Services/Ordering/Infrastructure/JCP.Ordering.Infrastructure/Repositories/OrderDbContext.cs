using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderDbContext : DbContext
    {
        public const string ORDERING_SCHEMA = "Ordering";
        public const string CATALOG_SCHEMA = "Catalog";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderOrderItem> OrderOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Used fluent API, Data Annotations could be used instead of Fluent API
            builder.ApplyConfiguration(new OrderItemEntityConfiguration());
            builder.ApplyConfiguration(new OrderEntityConfiguration());
            builder.ApplyConfiguration(new OrderOrderItemEntityConfiguration());
        }
    }
}