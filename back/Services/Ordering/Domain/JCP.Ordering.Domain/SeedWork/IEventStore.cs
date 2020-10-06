using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IEventStore
    {
        IEnumerable<INotification> GetEvents(string streamName);
        void PersistEvents(string streamName, IEnumerable<INotification> domainEvents);
    }
}
