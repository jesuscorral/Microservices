using JCP.Catalog.Domain.Model;
using JCP.Catalog.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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

        public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogDbContext>
        {
            public CatalogDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>()
                    .UseSqlServer("Server=.;Initial Catalog=Catalog;Integrated Security=true");

                return new CatalogDbContext(optionsBuilder.Options);
            }
        }
    }
}
