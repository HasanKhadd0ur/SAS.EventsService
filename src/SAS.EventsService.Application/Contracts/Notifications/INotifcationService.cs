using SAS.EventsService.Application.Notifications.Common;
namespace SAS.EventsService.Application.Contracts.Notfications
{
    public interface INotificationService
    {
        Task NotifyUserAsync(Guid userId, EventNotificationDTO notification);
    }

}
