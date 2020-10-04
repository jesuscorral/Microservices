using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        private readonly DomainEventApplierRegistry _domainEventApplierRegistry;
        private readonly IList<IDomainEvent> _changes;
        public Guid Id { get; set; }
        public int Version { get; private set; }

        protected AggregateRoot(Guid id) {
            Id = id;

            _domainEventApplierRegistry = new DomainEventApplierRegistry();
            _changes = new List<IDomainEvent>();

            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterDomainEventAppliers();
        }

        protected AggregateRoot(Guid id, IEnumerable<IDomainEvent> domainEvents)
             : this(id) {
            foreach (var domainEvent in domainEvents) {
                ApplyDomainEvent(domainEvent, true);
            }
        }

        protected void ApplyDomainEvent(IDomainEvent domainEvent, bool isPrevious = false) {
            var applier = _domainEventApplierRegistry.Find(domainEvent);
            applier.Invoke(domainEvent);

            Version++;

            if (isPrevious) {
                return;
            }

            _changes.Add(domainEvent);
        }

        protected abstract void RegisterDomainEventAppliers();

        protected void RegisterDomainEventApplier<TDomainEvent>(Action<TDomainEvent> applier)
           where TDomainEvent : class, IDomainEvent {
            _domainEventApplierRegistry.Register(applier);
        }

        public void ConsumeDomainEventChanges(IDomainEventsConsumer domainEventsConsumer) {
            if (!_changes.Any()) {
                return;
            }
            domainEventsConsumer.Consume(this, _changes);
            _changes.Clear();
        }
    }
}
