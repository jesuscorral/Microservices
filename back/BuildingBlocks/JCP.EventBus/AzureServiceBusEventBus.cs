using System;
using System.Text;
using System.Threading.Tasks;
using JCP.EventBus.Events;
using JCP.EventBus.Events.Interfaces;
using JCP.EventBus.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JCP.EventBus
{
    public class AzureServiceBusEventBus : IEventBus
    {
        private readonly SubscriptionClient subscriptionClient;
        private readonly IEventBusSubscriptionsManager subscriptionManager;
        private readonly IServiceBusConnectionManagementService serviceBusConnectionManagementService;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<AzureServiceBusEventBus> logger;

        public AzureServiceBusEventBus(IServiceBusConnectionManagementService serviceBusConnectionManagementService,
                        IEventBusSubscriptionsManager subscriptionManager,
                        IServiceProvider serviceProvider,
                        ILogger<AzureServiceBusEventBus> logger,
                        string subscriptionClientName)
        {
            this.serviceBusConnectionManagementService = serviceBusConnectionManagementService;
            this.subscriptionManager = subscriptionManager;
            this.subscriptionClient = subscriptionClient = new SubscriptionClient(this.serviceBusConnectionManagementService.ServiceBusConnectionStringBuilder,
                subscriptionClientName);
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public async Task SetupAsync()
        {
            try
            {
                await RemoveDefaultRuleAsync();
                RegisterSubscriptionClientMessageHandler();
            }

            catch (MessagingEntityNotFoundException)
            {
                logger.LogWarning("The messaging entity '{DefaultRuleName}' Could not be found.", RuleDescription.DefaultRuleName);
            }
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name;
            var jsonMessage = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = eventName,
            };

            var topicClient = serviceBusConnectionManagementService.CreateTopicClient();
            await topicClient.SendAsync(message);
        }

        public async Task SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            var containsKey = subscriptionManager.HasSubscriptionsForEvent<T>();
            if (!containsKey)
            {
                try
                {
                    await subscriptionClient.AddRuleAsync(new RuleDescription
                    {
                        Filter = new CorrelationFilter { Label = eventName },
                        Name = eventName
                    });
                }
                catch (ServiceBusException)
                {
                    logger.LogWarning("The messaging entity '{eventName}' already exists.", eventName);
                }
            }

            logger.LogInformation("Subscribing to event '{EventName}' with '{EventHandler}'", eventName, typeof(TH).Name);
            subscriptionManager.AddSubscription<T, TH>();
        }

        public async Task UnsubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            try
            {
                await subscriptionClient.RemoveRuleAsync(eventName);
            }
            catch (MessagingEntityNotFoundException)
            {
                logger.LogWarning("The messaging entity '{eventName}' could not be found.", eventName);
            }

            logger.LogInformation("Unsubscribing from event '{EventName}'", eventName);
            subscriptionManager.RemoveSubscription<T, TH>();
        }

        private void RegisterSubscriptionClientMessageHandler()
        {
            subscriptionClient.RegisterMessageHandler(
                async (message, token) => {
                    var eventName = message.Label;
                    var messageData = Encoding.UTF8.GetString(message.Body);

                    if (await ProcessEvent(eventName, messageData))
                    {
                        await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                },
                new MessageHandlerOptions(ExceptionReceivedHandler) { MaxConcurrentCalls = 10, AutoComplete = false });
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var ex = exceptionReceivedEventArgs.Exception;
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            logger.LogError(ex, "ERROR handling message: '{ExceptionMessage}' - Context: '{ExceptionContext}'", ex.Message, context);

            return Task.CompletedTask;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;
            if (subscriptionManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = subscriptionManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = serviceProvider.GetRequiredService(subscription.HandlerType);
                    if (handler == null) continue;

                    var eventType = subscriptionManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
                    processed = true;
                }
            }

            return processed;
        }

        private async Task RemoveDefaultRuleAsync()
        {
            try
            {
                await subscriptionClient.RemoveRuleAsync(RuleDescription.DefaultRuleName);
            }
            catch (MessagingEntityNotFoundException)
            {
                logger.LogWarning("The messaging entity '{DefaultRuleName}' Could not be found.", RuleDescription.DefaultRuleName);
            }
        }
    }
}
