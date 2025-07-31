using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.SharedKernel.DomainExceptions.Base;
using SAS.SharedKernel.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.Events.DomainExceptions;
using SAS.EventsService.Domain.Common.Errors;

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
        public ICollection<Review> Reviews { get; set; }

        public Event()
        {
            Reviews = new List<Review>();
            Messages = new List<Message>();
            NamedEntityMentions = new List<NamedEntityMention>();
            MentionedEntities = new List<NamedEntity>();
            IsReviewed = false;
        }

        public void AddMessage(Message message)
        {
            if (message is null) {
                throw EventExceptions.MessageNull();
            }
            Messages.Add(message);

        }
        public void AddNamedEntityMention(NamedEntity entity)
        {
            if (entity is null)
                throw EventExceptions.NamedEntityNull();

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
        public void MarkAsReviewed()
        {
            IsReviewed = true;
            UpdateLastModifiedTime(DateTime.UtcNow);
        }

        public void UpdateEventInfo(EventInfo newInfo)
        {
            
            EventInfo = newInfo;

            UpdateLastModifiedTime(DateTime.UtcNow);
        }
        public void UpdateLocation(Location newLocation)
        {
            if (newLocation is null)
                throw EventExceptions.LocationNull();

            Location = newLocation;
        }

        public void UpdateLastModifiedTime(DateTime modificationTime)
        {
            LastUpdatedAt = modificationTime;
        }

        public void ChangeTopic(Topic newTopic)
        {
            if (newTopic is null)
                throw EventExceptions.TopicNull();
            Topic = newTopic;
            UpdateLastModifiedTime(DateTime.UtcNow);
        }
        public void AddReview(Guid userId, string userName, string comment)
        {
            
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Event = this,
                EventId = this.Id,
                UserId = userId,
                UserName = userName,
                Comment = comment
            };

            Reviews.Add(review);
            UpdateLastModifiedTime(DateTime.UtcNow);
        }

    }
}
