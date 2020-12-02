using JCP.Catalog.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCP.Catalog.Infrastructure.EntityConfigurations
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable<CatalogItem>("CatalogItem", "Catalog");

            //builder.Property(ci => ci.Id)
            //    .UseHiLo("catalog_hilo")
            //    .IsRequired();

            builder.Property(ci => ci.Name)
                    .IsRequired(true)
                    .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                    .IsRequired(true)
                    .HasColumnType("decimal")
                    .HasPrecision(4,2);
        }
    }
}