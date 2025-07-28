using FluentAssertions;
using SAS.EventsService.Infrastructure.Services;

namespace SAS.EventsService.Tests.UnitTests.Events.Infrastructure.Services.Providers
{
    public class IdProviderTests
    {
        [Fact]
        public void GenerateId_WithSameInput_ReturnsSameGuid()
        {
            var provider = new IdProvider();

            var id1 = provider.GenerateId<string>("TestKey");
            var id2 = provider.GenerateId<string>("testkey "); // case and spaces ignored in implementation

            id1.Should().Be(id2);
        }

        [Fact]
        public void GenerateNewId_ReturnsNonEmptyGuid()
        {
            var provider = new IdProvider();

            var id = provider.GenerateNewId();

            id.Should().NotBe(Guid.Empty);
        }
    }

}