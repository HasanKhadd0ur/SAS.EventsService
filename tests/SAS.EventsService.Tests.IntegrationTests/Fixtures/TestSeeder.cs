using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;

namespace SAS.EventsService.Tests.IntegrationTests.Events
{
    public static class TestSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Topics.Add(new Topic
            {
                Id = Guid.NewGuid(),
                Name = "ValidTopic",
                Description = "A valid topic",
                IconUrl = "https://example.com/icon.png"
            });

            context.Regions.Add(new Region
            {
                Id = Guid.NewGuid(),
                Name = "RegionName"
            });

            context.SaveChanges();
        }
    }
}
