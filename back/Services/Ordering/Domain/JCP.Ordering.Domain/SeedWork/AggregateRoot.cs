using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
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

        private readonly DomainEventApplierRegistry _domainEventApplierRegistry;
        private readonly IList<IDomainEvent> _domainEvents;

        protected AggregateRoot() {
            //Id = id;

            _domainEventApplierRegistry = new DomainEventApplierRegistry();
            _domainEvents = new List<IDomainEvent>();

            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterDomainEventAppliers();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent) {
            var applier = _domainEventApplierRegistry.Find(domainEvent);
            applier.Invoke(domainEvent);

            _domainEvents.Add(domainEvent);
        }

        protected abstract void RegisterDomainEventAppliers();

        protected void RegisterDomainEventApplier<TDomainEvent>(Action<TDomainEvent> applier)
           where TDomainEvent : class, IDomainEvent {
            _domainEventApplierRegistry.Register(applier);
        }

        public void ConsumeDomainEventChanges(IDomainEventsConsumer domainEventsConsumer) {
            if (!_domainEvents.Any()) {
                return;
            }
            domainEventsConsumer.Consume(this, _domainEvents);
            _domainEvents.Clear();
        }
    }
}
