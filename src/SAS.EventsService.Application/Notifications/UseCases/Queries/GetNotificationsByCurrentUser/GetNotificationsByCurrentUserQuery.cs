
using Ardalis.Result;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Notifications.UseCases.Queries.GetNotificationsByCurrentUser
{
    public record GetNotificationsByCurrentUserQuery(
   int? PageNumber,
   int? PageSize) : IQuery<Result<ICollection<NotificationDTO>>>;
}
