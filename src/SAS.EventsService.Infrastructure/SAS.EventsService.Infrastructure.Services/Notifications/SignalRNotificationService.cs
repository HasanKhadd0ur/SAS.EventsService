using Microsoft.AspNetCore.SignalR;
using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.Infrastructure.SignalR;

namespace SAS.EventsService.Infrastructure.Services.Notifications
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUserAsync(Guid userId, EventNotificationDTO notification)
        {
            await _hubContext
                .Clients
                .User(userId.ToString())
                .SendAsync("EventNearby", notification);
        }
    }
}
