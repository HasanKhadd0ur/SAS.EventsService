using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.Regions.Entities;

namespace SAS.EventsService.UnitTests.Common
{
    public abstract class EventTestBase
    {
            protected Event CreateDefaultEvent()
            {
                return EventFactory.CreateValidEvent();
            }
            protected EventFactory Factory => new();

            protected void AssertValid<T>(T actual, T expected)
            {
                Assert.NotNull(actual);
                Assert.Equal(expected, actual);
            }
        }
    }
}
