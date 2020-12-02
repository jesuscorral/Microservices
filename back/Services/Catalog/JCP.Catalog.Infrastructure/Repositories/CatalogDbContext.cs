using JCP.Catalog.Domain.Model;
using JCP.Catalog.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JCP.Catalog.Infrastructure.Repositories
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Used fluent API, Data Annotations could be used instead of Fluent API
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }

    }
}
