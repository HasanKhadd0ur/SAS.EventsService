using MediatR;
using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.Application.Notifications.UseCases.Commands.AddEventNotificationCommand;
using SAS.EventsService.Domain.Events.DomainEvents;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.SharedKernel.DomainEvents;
using System.Threading;
using System.Threading.Tasks;

public class NotifyUserOnEventCreatedHandler : IDomainEventHandler<EventDetected>
{
    private readonly IUserInterestsRepository _interestRepo;
    private readonly INotificationService _notificationService;
    private readonly IMediator _mediator;

    public NotifyUserOnEventCreatedHandler(
        IUserInterestsRepository interestRepo,
        INotificationService notificationService,
        IMediator mediator)
    {
        _interestRepo = interestRepo;
        _notificationService = notificationService;
        _mediator = mediator;
    }

    public async Task Handle(EventDetected domainEvent, CancellationToken cancellationToken)
    {
        var interests = await _interestRepo.GetNearbyUserInterests(domainEvent.Latitude, domainEvent.Longitude);

        foreach (var interest in interests)
        {
            var notificationDto = new EventNotificationDTO
            {
                UserId = interest.UserId,
                EventId = domainEvent.EventId,
                Title = domainEvent.Title,
                Latitude = domainEvent.Latitude,
                Longitude = domainEvent.Longitude,
                OccurredAt = domainEvent.CreatedAt,
                InterestName = interest.InterestName
            };

            // Send via NotificationService (e.g., email or UI toast)
            await _notificationService.NotifyUserAsync(interest.UserId, notificationDto);

            // Persist notification using command
            var command = new AddEventNotificationCommand(notificationDto);
            await _mediator.Send(command, cancellationToken);
        }
    }
}
