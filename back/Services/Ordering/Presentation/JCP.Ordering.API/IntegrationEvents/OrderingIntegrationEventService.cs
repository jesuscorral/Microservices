using System;
using System.Text;
using System.Threading.Tasks;
using JCP.Ordering.Domain.IntegrationEvents.Events;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace JCP.Ordering.API.IntegrationEvents
{
    // TODO - Mejorar la forma en que se envían los eventos al service bus, utilizando un nuget para guardar la configuracion personalizad del service bus
    public class OrderingIntegrationEventService : IOrderingIntegrationEventService
    {
        const string ServiceBusConnectionString = "Endpoint=sb://jcp-scrm.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=6m+j0fmghtSTazWEr9vd4sAgVFgYJ62bGIbADGePSvQ=";
        const string TopicName = "jcp-topic";
        static ITopicClient topicClient;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";

        public async Task PublishEventsThroughEventBusAsync(IntegrationEvent evt)
        {
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            await SendMessagesAsync(evt);

            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(IntegrationEvent evt)
        {
            try {
                var eventName = evt.GetType().Name.Replace(INTEGRATION_EVENT_SUFFIX, "");
                var jsonMessage = JsonConvert.SerializeObject(evt);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                var message = new Message {
                    MessageId = Guid.NewGuid().ToString(),
                    Body = body,
                    Label = eventName,
                };

                // Send the message to the topic.
                await topicClient.SendAsync(message);
            
            }
            catch (Exception exception) {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}