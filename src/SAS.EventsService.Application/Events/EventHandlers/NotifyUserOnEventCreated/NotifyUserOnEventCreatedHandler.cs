using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Domain.Events.DomainEvents;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.SharedKernel.DomainEvents;

public class NotifyUserOnEventCreatedHandler : IDomainEventHandler<EventDetected>
{
    private readonly IUserInterestsRepository _interestRepo;
    private readonly INotificationService _notificationService;

    public NotifyUserOnEventCreatedHandler(IUserInterestsRepository interestRepo, INotificationService notificationService)
    {
        _interestRepo = interestRepo;
        _notificationService = notificationService;
    }

    public async Task Handle(EventDetected domainEvent, CancellationToken cancellationToken)
    {
        var interests = await _interestRepo.GetNearbyUserInterests(domainEvent.Latitude, domainEvent.Longitude);

        foreach (var interest in interests)
        {
            await _notificationService.NotifyUserAsync(interest.UserId, domainEvent.EventId);
        }
    }
}
