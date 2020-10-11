using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.DomainEvents;
using MediatR;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMediator _mediator;

        public OrderRepository(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }

        private List<BaseEvent> _domainEvents;
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<BaseEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
