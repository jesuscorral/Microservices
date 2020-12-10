using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCP.Ordering.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable<OrderItem>(nameof(OrderItem), OrderDbContext.ORDERING_SCHEMA);

            builder.HasKey(sc => new { sc.OrderId, sc.ProductId });

            builder.HasOne<Order>(o => o.Order)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(sc => sc.OrderId);

            builder.HasOne<Product>(oi => oi.Product)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
