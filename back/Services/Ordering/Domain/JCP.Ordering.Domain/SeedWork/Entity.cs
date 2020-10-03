using System;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class Entity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}