using System;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        //private List<INotification> _domainEvents;
        //public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        private Guid _Id;
        [JsonProperty(PropertyName = "id")]
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        //protected void AddDomainEvent(INotification domainEvent)
        //{
        //    _domainEvents = _domainEvents ?? new List<INotification>();
        //    _domainEvents.Add(domainEvent);
        //}

        //public void ClearDomainEvents()
        //{
        //    _domainEvents?.Clear();
        //}
    }
}
