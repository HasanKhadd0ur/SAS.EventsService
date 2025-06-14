using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.DomainExceptions.Base;
using SAS.EventsService.SharedKernel.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class Event : BaseEntity<Guid>
    {

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public EventInfo EventInfo { get; set; }
        public Topic Topic { get; set; }
        public Location Location { get; set; }
        public Region Region { get; set; }
        public ICollection<NamedEntity> MentionedEntities { get; set; }
        public ICollection<NamedEntityMention> NamedEntityMentions { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Event()
        {
            Messages = new List<Message>();
        }

        public void AddMessage(Message message)
        {
            if (message == null) {
                throw new DomainException("Message Should not be null");
            }
            Messages.Add(message);

        }
        public void UpdateEventInfo(EventInfo newInfo)
        {
            
            EventInfo = newInfo;

            UpdateLastModifiedTime(DateTime.UtcNow);
        }
        public void UpdateLocation(Location newLocation)
        {
            if (newLocation == null)
                throw new DomainException("Location must not be null");

            Location = newLocation;
        }

        public void UpdateLastModifiedTime(DateTime modificationTime)
        {
            LastUpdatedAt = modificationTime;
        }

    }
}
