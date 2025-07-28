using SAS.EventService.Domain.Entities;

namespace SAS.EventsService.Tests.UnitTests.Topics.Shared
{
    public abstract class TopicTestBase
    {
        protected Topic CreateDefaultEvent()
        {
            return TopicFactory.Create();
        }
        protected TopicFactory TopicFactory { get; set; }

        protected void AssertValid<T>(T actual, T expected)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }
    }

}