using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JCP.Ordering.Domain.DomainEvents;
using JCP.Ordering.Domain.SeedWork;
using MediatR;
using Newtonsoft.Json;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class CosmosDbRepository<T> : IRepository<T> where T : IDomainEvent
    {
        private readonly IMediator _mediator;

        private List<IDomainEvent> _domainEvents;
        //public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public CosmosDbRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }
        public async Task<T> AddDomainEvent(T domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
            return JsonConvert.DeserializeObject<T>("");

        }
        public async Task<bool> SaveEntities()
        {
            // Save to DB

            foreach (var domainEvent in _domainEvents)
                await _mediator.Publish(domainEvent);

            return true;
        }

        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }
    }
}
