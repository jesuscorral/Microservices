using System.Collections.Generic;
using System.Linq;
using JCP.Ordering.Domain.SeedWork;

namespace JCP.Ordering.Infrastructure
{
    public class InMemoryEventStore : IEventStore
    {

        private readonly IDictionary<string, IList<IDomainEvent>> _streams =
            new Dictionary<string, IList<IDomainEvent>>();

        public IEnumerable<IDomainEvent> GetEvents(string streamName) {
            return _streams[streamName];
        }

        // TODO - Sustituir por Cosmos DB
        public void PersistEvents(string streamName, IEnumerable<IDomainEvent> domainEvents) {
            var isExisting = _streams.TryGetValue(streamName, out var storedEvents);
            if (!isExisting) {
                _streams.Add(streamName, domainEvents.ToList());
            }
            else {
                var events = storedEvents.ToList();
                events.AddRange(domainEvents);
                _streams[streamName] = events;
            }
        }
    }
}