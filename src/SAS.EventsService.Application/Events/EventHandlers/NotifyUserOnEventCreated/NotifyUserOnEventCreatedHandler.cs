using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Domain.Events.DomainEvents;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.SharedKernel.DomainEvents;
using System.Threading.Tasks;
using System.Threading;

public class NotifyUserOnEventCreatedHandler : IDomainEventHandler<EventDetected>
{
    private readonly IUserInterestsRepository _interestRepo;
    private readonly INotificationService _notificationService;

    public NotifyUserOnEventCreatedHandler(
            IUserInterestsRepository interestRepo,
            INotificationService notificationService)
    {
        _interestRepo = interestRepo;
        _notificationService = notificationService;
    }

    public async Task Handle(EventDetected domainEvent, CancellationToken cancellationToken)
    {
        var interests = await _interestRepo.GetNearbyUserInterests(domainEvent.Latitude, domainEvent.Longitude);

        foreach (var interest in interests)
        {
            var notification = new EventNotification
            {
                EventId = domainEvent.EventId,
                Title = domainEvent.Title,
                Latitude = domainEvent.Latitude,
                Longitude = domainEvent.Longitude,
                OccurredAt = domainEvent.CreatedAt,
            };

            Console.WriteLine($"Sending notification for EventId: {domainEvent.EventId} to UserId: {interest.UserId}");

            await _notificationService.NotifyUserAsync(interest.UserId, notification);
        }
    }
}
