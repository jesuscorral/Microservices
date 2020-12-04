using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCP.EventBus.Events;
using JCP.EventBus.Events.Interfaces;

namespace JCP.EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> handlers;
        private readonly List<Type> eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            handlers = new Dictionary<string, List<SubscriptionInfo>>();
            eventTypes = new List<Type>();
        }

        public bool IsEmpty => !handlers.Keys.Any();

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            if (!HasSubscriptionsForEvent(eventName))
            {
                handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            var handlerType = typeof(TH);

            if (handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type '{handlerType.Name}' already registered for '{eventName}'",
                    nameof(handlerType));
            }

            handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));

            if (!eventTypes.Contains(typeof(T)))
            {
                eventTypes.Add(typeof(T));
            }
        }

        public void Clear() => handlers.Clear();

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type GetEventTypeByName(string eventName) => eventTypes
                                                            .SingleOrDefault(t => t.Name == eventName);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => handlers[eventName];

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }

        public bool HasSubscriptionsForEvent(string eventName) => handlers.ContainsKey(eventName);

        public void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            if (handlerToRemove != null)
            {
                handlers[eventName].Remove(handlerToRemove);
                if (!handlers[eventName].Any())
                {
                    handlers.Remove(eventName);
                    var eventType = eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
             where T : IntegrationEvent
             where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return handlers[eventName].SingleOrDefault(s => s.HandlerType == typeof(TH));
        }
    }
}
