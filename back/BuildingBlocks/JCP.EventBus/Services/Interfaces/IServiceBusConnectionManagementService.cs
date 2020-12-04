using Microsoft.Azure.ServiceBus;

namespace JCP.EventBus.Services.Interfaces
{
    public interface IServiceBusConnectionManagementService
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateTopicClient();
    }
}