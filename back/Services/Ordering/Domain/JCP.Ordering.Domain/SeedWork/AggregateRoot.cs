using System;
using Newtonsoft.Json;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        private Guid _Id;
        [JsonProperty(PropertyName = "id")]
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }
    }
}
