using SAS.EventsService.Application.Contracts.Notfications;

namespace SAS.EventsService.Infrastructure.Services.Notifications
{
    public class SignalRNotificationService : INotificationService
    {
        //private readonly IHubContext<NotificationHub> _hubContext;

        //public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        //{ 
        //    _hubContext = hubContext;
        //}

        public SignalRNotificationService()
        {

        }

        public async  Task NotifyUserAsync(Guid userId, Guid eventId)
        {
            //await _hubContext.Clients.User(userId.ToString())
            //    .SendAsync("EventNearby", new { EventId = eventId });
             Console.WriteLine("dsfdfsdsfdfdsfdsfds");
        }
    }
}