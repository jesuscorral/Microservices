using System;
using JCP.EventBus.Services.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace JCP.EventBus.Services
{
    public class ServiceBusConnectionManagementService : IServiceBusConnectionManagementService
    {
        private readonly ILogger<ServiceBusConnectionManagementService> logger;
        private readonly ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder;
        private ITopicClient topicClient;

        public ServiceBusConnectionManagementService(ILogger<ServiceBusConnectionManagementService> logger,
                               ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ?? throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            this.topicClient = new TopicClient(this.serviceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => serviceBusConnectionStringBuilder;

        public ITopicClient CreateTopicClient()
        {
            if (topicClient.IsClosedOrClosing)
            {
                topicClient = new TopicClient(serviceBusConnectionStringBuilder, RetryPolicy.Default);
            }
            return topicClient;
        }
    }
}
