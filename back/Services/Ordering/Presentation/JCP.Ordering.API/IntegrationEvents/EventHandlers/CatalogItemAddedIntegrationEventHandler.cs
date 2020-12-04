using System;
using System.Threading.Tasks;
using JCP.EventBus.Events.Interfaces;
using JCP.Ordering.Api.IntegrationEvents.Events;
using JCP.Ordering.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace JCP.Ordering.Api.IntegrationEvents.EventHandlers
{
    public class CatalogItemAddedIntegrationEventHandler : IIntegrationEventHandler<CatalogItemAddedIntegrationEvent>
    {
        private readonly ILogger<CatalogItemAddedIntegrationEventHandler> _logger;
        private readonly IOrderingRepository _reservationRepository;

        public CatalogItemAddedIntegrationEventHandler(ILogger<CatalogItemAddedIntegrationEventHandler> logger,
                                                            IOrderingRepository reservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        }

        public async Task HandleAsync(CatalogItemAddedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);


            //var userIds = _reservationRepository.GetUsers();

            //foreach (var id in userIds)
            //{
            //    var customerReservation = await _reservationRepository.GetReservationAsync(id);

            await UpdatePriceInCustomerReservation(@event.Id);

        }

        private async Task UpdatePriceInCustomerReservation(Guid id)
        {
            //if (Id == reservation.Car.Id)
            //{
            //    _logger.LogInformation($"{nameof(CatalogItemAddedIntegrationEventHandler)} - Updating car price in reservation for the customer: {reservation.CustomerId}", reservation.CustomerId);

            //    if (reservation.Car.PricePerDay == oldPrice)
            //    {
            //        reservation.Car.PricePerDay = newPrice;
            //    }
            await _reservationRepository.SaveOrderItemAsync(id);
            //}
        }
    }
}
