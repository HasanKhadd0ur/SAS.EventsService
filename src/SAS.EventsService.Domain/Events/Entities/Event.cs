using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.DomainExceptions.Base;
using SAS.EventsService.SharedKernel.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using SAS.EventsService.Domain.NamedEntities.Entities;

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
        public Boolean IsReviewed { get; set; }
        public ICollection<NamedEntity> MentionedEntities { get; set; }
        public ICollection<NamedEntityMention> NamedEntityMentions { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Event()
        {
            Messages = new List<Message>();
            NamedEntityMentions = new List<NamedEntityMention>();

        }

        public void AddMessage(Message message)
        {
            if (message == null) {
                throw new DomainException("Message Should not be null");
            }
            Messages.Add(message);

        }
        public void AddNamedEntityMention(NamedEntity entity)
        {
            if (entity == null)
                throw new DomainException("NamedEntity cannot be null");

            if (NamedEntityMentions.Any(m => m.NamedEntityId == entity.Id)) return;

            NamedEntityMentions.Add(new NamedEntityMention
            {
                EventId = this.Id,
                NamedEntityId = entity.Id,
                NamedEntity = entity,
                Event = this
            });

            MentionedEntities.Add(entity);
        }
        public void UpdateEventInfo(EventInfo newInfo)
        {
            
            EventInfo = newInfo;

            UpdateLastModifiedTime(DateTime.UtcNow);
        }
        public void UpdateLocation(Location newLocation)
        {
            if (newLocation is null)
                throw new DomainException("Location must not be null");

            Location = newLocation;
        }

        public void UpdateLastModifiedTime(DateTime modificationTime)
        {
            LastUpdatedAt = modificationTime;
        }

    }
}
