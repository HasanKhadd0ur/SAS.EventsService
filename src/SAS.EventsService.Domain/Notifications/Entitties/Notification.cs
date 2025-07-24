using SAS.EventsService.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Domain.Notifications.Entitties
{
    public abstract class Notification : BaseEntity<Guid>
    {
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public bool IsRead { get; private set; }
        public string Type { get; protected set; }

        protected Notification(Guid userId, NotificationType type)
        {
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
            Type = type.ToString();  
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }

    public class EventNotification : Notification
    {
        public Guid EventId { get; private set; }
        public string Title { get; private set; }
        public string InterestName { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime OccurredAt { get; private set; }

        public EventNotification(Guid userId, Guid eventId, string title, double latitude, double longitude, DateTime occurredAt, string interestName)
       : base(userId, NotificationType.Event)
        {
            EventId = eventId;
            Title = title;
            Latitude = latitude;
            Longitude = longitude;
            OccurredAt = occurredAt;
            InterestName = interestName;
        }
    }

    public enum NotificationType
    {
        Event,
      
    }
}
