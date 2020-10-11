using System;
using MediatR;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.DomainEvents
{
    public abstract class BaseEvent : INotification
    {
        [JsonProperty(PropertyName = "id")]
        public Guid EventId { get; set; }
    }
}
