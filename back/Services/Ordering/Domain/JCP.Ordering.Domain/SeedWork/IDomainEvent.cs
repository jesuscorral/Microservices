using System;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}
