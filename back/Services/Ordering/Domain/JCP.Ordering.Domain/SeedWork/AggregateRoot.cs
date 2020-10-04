using System.Collections.Generic;
using MediatR;

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

        //private readonly DomainEventApplierRegistry _domainEventApplierRegistry;
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        //protected AggregateRoot() {
        //    //Id = id;

        //    _domainEventApplierRegistry = new DomainEventApplierRegistry();
        //    _domainEvents = new List<INotification>();

        //    // ReSharper disable once VirtualMemberCallInConstructor
        //    RegisterDomainEventAppliers();
        //}

        protected void AddDomainEvent(INotification domainEvent) {
            //var applier = _domainEventApplierRegistry.Find(domainEvent);
            //applier.Invoke(domainEvent);

            //_domainEvents.Add(domainEvent);

            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        //protected abstract void RegisterDomainEventAppliers();

        //protected void RegisterDomainEventApplier<TDomainEvent>(Action<TDomainEvent> applier)
        //   where TDomainEvent : class, IDomainEvent {
        //    _domainEventApplierRegistry.Register(applier);
        //}

        //public void ConsumeDomainEventChanges(IDomainEventsConsumer domainEventsConsumer) {
        //    if (!_domainEvents.Any()) {
        //        return;
        //    }
        //    domainEventsConsumer.Consume(this, _domainEvents);
        //    _domainEvents.Clear();
        //}
    }
}
