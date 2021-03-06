﻿using JCP.Catalog.Domain.Model;
using JCP.Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCP.Catalog.Infrastructure.EntityConfigurations
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable<Product>(nameof(Product), CatalogDbContext.CATALOG_SCHEMA);

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