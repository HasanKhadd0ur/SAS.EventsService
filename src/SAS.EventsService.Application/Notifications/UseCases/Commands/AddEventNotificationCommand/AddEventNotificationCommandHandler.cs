
using Ardalis.Result;
using SAS.EventsService.Domain.Notifications.Entitties;
using SAS.EventsService.Domain.Notifications.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Notifications.UseCases.Commands.AddEventNotificationCommand
{
    public class AddEventNotificationCommandHandler : ICommandHandler<AddEventNotificationCommand, Result<Guid>>
    {
        private readonly INotificationRepository _repository;

        public AddEventNotificationCommandHandler(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(AddEventNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.Notification;
                var notification = new EventNotification(
                    dto.UserId,
                    dto.EventId,
                    dto.Title,
                    dto.Latitude,
                    dto.Longitude,
                    dto.OccurredAt,
                    dto.InterestName);

                await _repository.AddAsync(notification);
                
                return Result.Success(notification.Id);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
