using System;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.Common;
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
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Used fluent API, Data Annotations could be used instead of Fluent API
            builder.ApplyConfiguration(new ProductEntityConfiguration());
            builder.ApplyConfiguration(new OrderEntityConfiguration());
            builder.ApplyConfiguration(new OrderItemEntityConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}