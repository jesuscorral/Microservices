namespace JCP.Catalog.Infrastructure.Configurations.Interfaces
{
    public interface IAzureServiceBusConfiguration
    {
        string ConnectionString { get; set; }
        string SubscriptionClientName { get; set; }
    }
}
