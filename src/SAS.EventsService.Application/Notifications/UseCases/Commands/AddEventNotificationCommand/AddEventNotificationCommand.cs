
using Ardalis.Result;
using SAS.EventsService.Application.Notifications.Common;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Notifications.UseCases.Commands.AddEventNotificationCommand
{
    public record AddEventNotificationCommand(EventNotificationDTO Notification) : ICommand<Result<Guid>>;
}
