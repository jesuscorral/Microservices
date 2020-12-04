using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JCP.EventBus.Events;
using JCP.EventLog.Entries;
using Microsoft.EntityFrameworkCore.Storage;

namespace JCP.EventLog.Services.Interfacces
{
    public interface IEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
