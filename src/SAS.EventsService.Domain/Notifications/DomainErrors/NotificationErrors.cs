using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Notifications.DomainErrors;

public class NotificationErrors
{
    public static readonly DomainError UnExistUser =
        new("Notification.UnExistUser", "There are no user ofr this id");

}