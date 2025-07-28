using FluentAssertions;
using SAS.EventsService.Tests.UnitTests.Topics.Shared;

namespace SAS.EventsService.Tests.UnitTests.Topics.Domain
{
    public class TopicTests
    {
        [Fact]
        public void Create_ShouldSetProperties()
        {
            var topic = TopicFactory.Create(
                name: "Politics",
                iconUrl: "https://icons.com/politics.png",
                description: "Political news and discussions"
            );

            topic.Name.Should().Be("Politics");
            topic.IconUrl.Should().Be("https://icons.com/politics.png");
            topic.Description.Should().Be("Political news and discussions");
            topic.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void UpdateName_ShouldChangeName()
        {
            var topic = TopicFactory.Create(name: "Old Name");
            topic.UpdateName("New Name");

            topic.Name.Should().Be("New Name");
        }

        [Fact]
        public void UpdateIconUrl_ShouldChangeIconUrl()
        {
            var topic = TopicFactory.Create(iconUrl: "old_url.png");
            topic.UpdateIconUrl("new_url.png");

            topic.IconUrl.Should().Be("new_url.png");
        }

        [Fact]
        public void UpdateDescription_ShouldChangeDescription()
        {
            var topic = TopicFactory.Create(description: "Old Desc");
            topic.UpdateDescription("New Desc");

            topic.Description.Should().Be("New Desc");
        }
    }
}
