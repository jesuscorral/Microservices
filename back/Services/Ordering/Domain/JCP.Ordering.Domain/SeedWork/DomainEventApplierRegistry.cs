using System;
using System.Collections.Generic;

namespace JCP.Ordering.Domain.SeedWork
{
    // TODO - Registrar los dominios al inicio
    public class DomainEventApplierRegistry
    {
        private readonly IDictionary<Type, Action<IDomainEvent>> _appliers =
            new Dictionary<Type, Action<IDomainEvent>>();

        public void Register<TDomainEvent>(Action<TDomainEvent> applier)
            where TDomainEvent : class, IDomainEvent {
            void Applier(IDomainEvent domainEvent) => applier((TDomainEvent)domainEvent);
            _appliers.Add(typeof(TDomainEvent), Applier);
        }

        public Action<IDomainEvent> Find(IDomainEvent domainEvent) {
            var domainEventType = domainEvent.GetType();
            var isFound = _appliers.TryGetValue(domainEventType, out var applier);
            if (!isFound) {
                throw new KeyNotFoundException($"Applier not registered for domain event type {domainEventType}");
            }

            return applier;
        }
    }
}