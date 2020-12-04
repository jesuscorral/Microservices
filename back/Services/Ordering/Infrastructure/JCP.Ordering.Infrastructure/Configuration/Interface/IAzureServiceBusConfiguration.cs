namespace JCP.Ordering.Infrastructure.Configuration.Interface
{
    public interface IAzureServiceBusConfiguration
    {
        string ConnectionString { get; set; }
        string SubscriptionClientName { get; set; }
    }
}
