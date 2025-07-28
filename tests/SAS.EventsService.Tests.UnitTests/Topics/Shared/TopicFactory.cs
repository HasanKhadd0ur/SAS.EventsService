using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.UnitTests.Common;

namespace SAS.EventsService.Tests.UnitTests.Topics.Shared
{
    public class TopicFactory
    {
        public static Topic Create(
            Guid? id = null,
            string name = "Default Topic",
            string iconUrl = "https://default.icon/url.png",
            string description = "Default description")
        {
            return new Topic
            {
                Id = id ?? Guid.NewGuid(),
                Name = name,
                IconUrl = iconUrl,
                Description = description
            };
        }
    }
}