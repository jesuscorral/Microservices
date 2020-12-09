using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCP.Ordering.Infrastructure.EntityConfigurations
{
    public class OrderOrderItemEntityConfiguration : IEntityTypeConfiguration<OrderOrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderOrderItem> builder)
        {
            builder.ToTable<OrderOrderItem>(nameof(OrderOrderItem), OrderDbContext.ORDERING_SCHEMA);

            builder.HasKey(sc => new { sc.OrderId, sc.OrderItemId });

            builder.HasOne<Order>(o => o.Order)
                .WithMany(s => s.OrderOrderItems)
                .HasForeignKey(sc => sc.OrderId);

            builder.HasOne<OrderItem>(oi => oi.OrderItem)
                .WithMany(s => s.OrderOrderItems)
                .HasForeignKey(so => so.OrderId);
        }
    }
}
