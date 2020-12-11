using System.Linq;
using System.Threading.Tasks;
using JCP.Ordering.Domain.Common;
using MediatR;

namespace JCP.Ordering.Infrastructure.Repositories
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<AuditableEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
