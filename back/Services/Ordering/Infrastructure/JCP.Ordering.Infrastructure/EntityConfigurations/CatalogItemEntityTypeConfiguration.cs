using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCP.Ordering.Infrastructure.EntityConfigurations
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable<OrderItem>("OrderItem", "Catalog");

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