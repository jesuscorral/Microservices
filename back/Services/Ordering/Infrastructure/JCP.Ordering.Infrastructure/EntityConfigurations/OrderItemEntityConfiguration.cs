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
            builder.ToTable<OrderItem>(nameof(OrderItem), OrderDbContext.CATALOG_SCHEMA);

            builder.Property(ci => ci.Name)
                    .IsRequired(true)
                    .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                    .IsRequired(true)
                    .HasColumnType("decimal")
                    .HasPrecision(4, 2);
        }
    }
}