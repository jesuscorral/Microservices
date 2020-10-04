using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCP.Ordering.Domain.SeedWork
{
    public interface IEventStore
    {
        IEnumerable<IDomainEvent> GetEvents(string streamName);
        void PersistEvents(string streamName, IEnumerable<IDomainEvent> domainEvents);
    }
}
