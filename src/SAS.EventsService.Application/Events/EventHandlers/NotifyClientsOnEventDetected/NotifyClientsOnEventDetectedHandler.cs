using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Domain.Events.DomainEvents;
using SAS.SharedKernel.DomainEvents;

namespace SAS.EventsService.Application.Events.EventHandlers.NotifyClientsOnEventDetected
{
    public class NotifyClientsOnEventDetectedHandler : IDomainEventHandler<EventDetected>
    {
        private readonly INotificationService _notificationService;

        public NotifyClientsOnEventDetectedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(EventDetected domainEvent, CancellationToken cancellationToken)
        {
            var payload = new Notifications.Common.EventNotificationDTO
            {
                Id = domainEvent.EventId,
                Title = domainEvent.Title,
                Latitude = domainEvent.Latitude,
                Longitude = domainEvent.Longitude,
                CreatedAt = domainEvent.CreatedAt
            };

            await _notificationService.SendEventDetectedAsync(payload);
        }
    }
}