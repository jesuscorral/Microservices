using System;
using System.Collections.Generic;
using MediatR;

namespace JCP.Ordering.Domain.SeedWork
{
    // TODO - Registrar los dominios al inicio
    public class DomainEventApplierRegistry
    {
        private readonly IDictionary<Type, Action<INotification>> _appliers =
            new Dictionary<Type, Action<INotification>>();

        public void Register<TDomainEvent>(Action<TDomainEvent> applier)
            where TDomainEvent : class, INotification
        {
            void Applier(INotification domainEvent) => applier((TDomainEvent)domainEvent);
            _appliers.Add(typeof(TDomainEvent), Applier);
        }

        public Action<INotification> Find(INotification domainEvent) 
        {
            var domainEventType = domainEvent.GetType();
            var isFound = _appliers.TryGetValue(domainEventType, out var applier);
            if (!isFound) 
            {
                throw new KeyNotFoundException($"Applier not registered for domain event type {domainEventType}");
            }

            return applier;
        }
    }
}