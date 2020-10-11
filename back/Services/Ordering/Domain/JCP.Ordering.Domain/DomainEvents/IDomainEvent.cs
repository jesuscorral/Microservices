using System;
using MediatR;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.DomainEvents
{
    public interface IDomainEvent : INotification
    {
        [JsonProperty(PropertyName = "id")]
        Guid EventId { get; }
    }
}
