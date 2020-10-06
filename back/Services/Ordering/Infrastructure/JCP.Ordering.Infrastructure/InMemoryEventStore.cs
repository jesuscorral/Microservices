using System.Collections.Generic;
using System.Linq;
using JCP.Ordering.Domain.SeedWork;
using MediatR;

namespace JCP.Ordering.Infrastructure
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly IDictionary<string, IList<INotification>> _streams =
            new Dictionary<string, IList<INotification>>();

        public IEnumerable<INotification> GetEvents(string streamName) {
            return _streams[streamName];
        }

        // TODO - Sustituir por Cosmos DB
        public void PersistEvents(string streamName, IEnumerable<INotification> domainEvents) {
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