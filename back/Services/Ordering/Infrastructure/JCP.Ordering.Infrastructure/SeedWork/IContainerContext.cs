using JCP.Ordering.Domain.SeedWork;
using Microsoft.Azure.Cosmos;

namespace JCP.Ordering.Infrastructure.SeedWork
{
    public interface IContainerContext<T> where T : AggregateRoot
    {
        string ContainerName { get; }
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
