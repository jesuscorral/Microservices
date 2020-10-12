namespace JCP.Ordering.Domain.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public OrderCreatedIntegrationEvent(string userId)
            => UserId = userId;
    }
}
