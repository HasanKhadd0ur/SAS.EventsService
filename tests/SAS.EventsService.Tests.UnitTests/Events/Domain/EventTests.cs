using FluentAssertions;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.SharedKernel.DomainExceptions.Base;

namespace SAS.EventsService.Tests.UnitTests.Events.Domain
{
    public class EventTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCollections()
        {
            var @event = new Event();

            @event.Messages.Should().NotBeNull();
            @event.NamedEntityMentions.Should().NotBeNull();
            @event.IsReviewed.Should().BeFalse();
        }

        [Fact]
        public void AddMessage_ShouldAddMessage_WhenValid()
        {
            var @event = new Event();
            var message = new Message { Content = "Test message" };

            @event.AddMessage(message);

            @event.Messages.Should().Contain(message);
        }

        [Fact]
        public void AddMessage_ShouldThrowException_WhenNull()
        {
            var @event = new Event();

            Action act = () => @event.AddMessage(null);

            act.Should().Throw<DomainException>()
               .WithMessage("Message Should not be null");
        }

        [Fact]
        public void AddNamedEntityMention_ShouldAddMention_WhenValid()
        {
            var @event = new Event();
            var namedEntity = new NamedEntity
            {
                Id = Guid.NewGuid(),
                EntityName = "Test",
                Type = new NamedEntityType { TypeName = "Organization" }
            };

            @event.AddNamedEntityMention(namedEntity);

            @event.NamedEntityMentions.Should().ContainSingle();
            @event.NamedEntityMentions.Should().Contain(m => m.NamedEntityId == namedEntity.Id);
        }

        [Fact]
        public void AddNamedEntityMention_ShouldNotAddDuplicate()
        {
            var @event = new Event();
            var entityId = Guid.NewGuid();

            var namedEntity = new NamedEntity { Id = entityId, EntityName = "Test" };

            @event.AddNamedEntityMention(namedEntity);
            @event.AddNamedEntityMention(namedEntity);

            @event.NamedEntityMentions.Should().HaveCount(1);
        }

        [Fact]
        public void AddNamedEntityMention_ShouldThrow_WhenEntityIsNull()
        {
            var @event = new Event();

            Action act = () => @event.AddNamedEntityMention(null);

            act.Should().Throw<DomainException>()
               .WithMessage("NamedEntity cannot be null");
        }

        [Fact]
        public void UpdateEventInfo_ShouldSetInfo_AndUpdateModifiedTime()
        {
            var @event = new Event();
            var newInfo = new EventInfo("Title", "Summary", 0.9, "Positive");

            var before = DateTime.UtcNow;
            @event.UpdateEventInfo(newInfo);
            var after = DateTime.UtcNow;

            @event.EventInfo.Should().Be(newInfo);
            @event.LastUpdatedAt.Should().BeAfter(before).And.BeBefore(after.AddSeconds(1));
        }

        [Fact]
        public void UpdateLocation_ShouldSetLocation_WhenValid()
        {
            var @event = new Event();
            var location = new Location { Latitude = 33.5, Longitude = 35.5 };

            @event.UpdateLocation(location);

            @event.Location.Should().Be(location);
        }

        [Fact]
        public void UpdateLocation_ShouldThrow_WhenNull()
        {
            var @event = new Event();

            Action act = () => @event.UpdateLocation(null);

            act.Should().Throw<DomainException>()
               .WithMessage("Location must not be null");
        }

        [Fact]
        public void MarkAsReviewed_ShouldSetIsReviewed_AndUpdateModifiedTime()
        {
            var @event = new Event();

            var before = DateTime.UtcNow;
            @event.MarkAsReviewed();
            var after = DateTime.UtcNow;

            @event.IsReviewed.Should().BeTrue();
            @event.LastUpdatedAt.Should().BeAfter(before).And.BeBefore(after.AddSeconds(1));
        }
    }
}
