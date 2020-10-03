using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace JCP.Ordering.Infrastructure.Context
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }

        protected OrderingContext() {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }

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
