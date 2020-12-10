using JCP.Ordering.Domain.Entities;
using JCP.Ordering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCP.Ordering.Infrastructure.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable<Order>(nameof(Order), OrderDbContext.ORDERING_SCHEMA);
        }
    }
}