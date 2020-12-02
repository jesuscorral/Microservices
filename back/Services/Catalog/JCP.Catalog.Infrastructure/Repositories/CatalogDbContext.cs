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
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }

    }
}
