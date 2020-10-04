using System.Configuration;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace JCP.Ordering.Infrastructure.Context
{
    public class OrderingContext : DbContext
    {
        private const string CONTAINER_NAME = "OrdersContainer";

        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options) {
            Database.EnsureCreated();
        }

        protected OrderingContext() {
            Database.EnsureCreated();
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
          
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.HasDefaultContainer(CONTAINER_NAME);
            modelBuilder.Entity<Order>().ToContainer(nameof(Order));
            //modelBuilder.Entity<Order>().HasPartitionKey(o => o.Name);
            modelBuilder.Entity<Order>().Property(p => p.Id).HasValueGenerator<GuidValueGenerator>();
            //modelBuilder.Entity<Order>().HasMany<OrderItem>(p => p.OrderItems);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

            });

        }
        // TODO - Implementar para no tener que meter cabeceras en los Objetos de dominio
        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
        //    modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
        //    modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        //    modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        //    modelBuilder.ApplyConfiguration(new CardTypeEntityTypeConfiguration());
        //    modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        //    modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
        //}
    }
}
