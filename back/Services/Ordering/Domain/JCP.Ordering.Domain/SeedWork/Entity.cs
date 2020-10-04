using System;
using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class Entity
    {
        //[JsonProperty(PropertyName = "id")]
        //public string Id { get; set; }
        int _Id;
        public virtual int Id
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
        private List<INotification> _domainEvents;
        private Guid id;
        private IEnumerable<IDomainEvent> domainEvents;

        protected Entity(Guid id, IEnumerable<IDomainEvent> domainEvents) {
            this.id = id;
            this.domainEvents = domainEvents;
        }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem) {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

    }
}