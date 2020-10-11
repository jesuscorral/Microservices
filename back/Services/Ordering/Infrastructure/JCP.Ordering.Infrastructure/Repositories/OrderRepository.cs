using JCP.Ordering.Domain.AggregatesModel.OrderAggregate;
using JCP.Ordering.Domain.DomainEvents;
using MediatR;

namespace JCP.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : CosmosDbRepository<IDomainEvent>, IOrderRepository
    {
        public override string CollectionName { get; } = "todoItems";

        public OrderRepository(IMediator mediator) : base(mediator) { }

        //    private readonly IMediator _mediator;

        //    public OrderRepository(IMediator mediator)
        //    {
        //        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        //    }

        //    //public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        //    //{
        //    //    await _mediator.DispatchDomainEventsAsync(this);

        //    //    return true;
        //    //}

        //    private List<BaseEvent> _domainEvents;
        //    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents?.AsReadOnly();

        //    public void AddDomainEvent(BaseEvent domainEvent)
        //    {
        //        _domainEvents = _domainEvents ?? new List<BaseEvent>();
        //        _domainEvents.Add(domainEvent);
        //    }

        //    public void ClearDomainEvents()
        //    {
        //        _domainEvents?.Clear();
        //    }

        //    public Task<Order> AddDomainEvent(Order order)
        //    {

        //        var domainEvent = new OrderCreatedEvent(Guid.NewGuid(), order);

        //        throw new NotImplementedException();
        //    }

        //    public Task<T> AddDomainEvent(T domainEvent)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
