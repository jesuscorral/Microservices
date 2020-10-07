using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        private Guid _Id;
        private readonly DomainEventApplierRegistry _domainEventApplierRegistry;
        private List<INotification> _domainEventsListChanges;
        //[JsonProperty(PropertyName = "id")]
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

        protected AggregateRoot(Guid id) 
        {
            _Id = id;
            _domainEventApplierRegistry = new DomainEventApplierRegistry();
            _domainEventsListChanges = new List<INotification>();

            // Registra el evento
            RegisterDomainEventAppliers();
        }

        protected void AddDomainEvent(INotification domainEvent)
        {
            var applier = _domainEventApplierRegistry.Find(domainEvent);
            applier.Invoke(domainEvent);

            _domainEventsListChanges.Add(domainEvent);
        }

        protected abstract void RegisterDomainEventAppliers();

        protected void RegisterDomainEventApplier<TDomainEvent>(Action<TDomainEvent> applier)
           where TDomainEvent : class, INotification 
        {
            _domainEventApplierRegistry.Register(applier);
        }

        public void ConsumeDomainEventChanges(IDomainEventsConsumer domainEventsConsumer) {
            if (!_domainEventsListChanges.Any()) {
                return;
            }
            domainEventsConsumer.Consume(this, _domainEventsListChanges);
            _domainEventsListChanges.Clear();
        }
    }
}
