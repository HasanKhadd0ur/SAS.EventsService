using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.Regions.Entities;

namespace SAS.EventsService.UnitTests.Common
{
    public class EventFactory
    {
        public static Event CreateValidEvent()
        {
            var eventInfo = CreateEventInfo();
            var location = CreateLocation();
            var topic = CreateTopic();
            var region = CreateRegion();

            return new Event
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                EventInfo = eventInfo,
                Location = location,
                Topic = topic,
                Region = region,
                MentionedEntities = new List<NamedEntity>(),
                NamedEntityMentions = new List<NamedEntityMention>(),
                Messages = new List<Message>()
            };
        }

        public static EventInfo CreateEventInfo(
            string title = "Protest in City",
            string summary = "People gathered to protest economic conditions.",
            double sentimentScore = 0.75,
            string sentimentLabel = "Positive")
        {
            return new EventInfo(title, summary, sentimentScore, sentimentLabel);
        }

        public static Location CreateLocation(
            string country = "Syria",
            string city = "Hama",
            double lat = 35.1318,
            double lon = 36.7578)
        {
            return new Location
            {
                Id = Guid.NewGuid(),
                Country = country,
                City = city,
                Latitude = lat,
                Longitude = lon
            };
        }

        public static Region CreateRegion(string name = "Syria - Hama")
        {
            return new Region
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static Topic CreateTopic(string name = "Protest")
        {
            return new Topic
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static NamedEntityType CreateEntityType(string typeName = "Organization", string normalizedName = "ORG")
        {
            return new NamedEntityType
            {
                Id = Guid.NewGuid(),
                TypeName = typeName,
                NormalisedName = normalizedName
            };
        }

        public static NamedEntity CreateNamedEntity(string name = "Red Cross")
        {
            var type = CreateEntityType();

            return new NamedEntity
            {
                Id = Guid.NewGuid(),
                EntityName = name,
                Type = type,
                TypeId = type.Id
            };
        }

        public static Message CreateMessage(string content = "Breaking news event.")
        {
            return new Message
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}