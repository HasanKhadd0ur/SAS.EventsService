using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Notifications.Common
{
    public class NotificationDTO :BaseDTO<Guid>
    {
        public Guid UserId { get; set; }
        public string Type { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }

    public class EventNotificationDTO : NotificationDTO
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public string InterestName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime OccurredAt { get; set; }
    }

}
