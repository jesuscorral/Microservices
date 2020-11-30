﻿using Catalog.API.Infrastructure.EntityConfigurations;
using Catalog.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Catalog.API.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }

        public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
        {
            public CatalogContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                    .UseSqlServer("Server=.;Initial Catalog=JCP.CatalogDb;Integrated Security=true");

                return new CatalogContext(optionsBuilder.Options);
            }
        }
    }
}