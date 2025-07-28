using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using Moq;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.DomainEvents;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.Domain.Regions.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.SharedKernel.Utilities;
using SAS.EventsService.UnitTests.Common;
using Xunit;

namespace SAS.EventsService.Tests.UnitTests.Events.Application.UseCases.Commands
{
    public class CreateEventFromDetectionCommandHandlerTests
    {
        private readonly Mock<ITopicsRepository> _topicRepoMock = new();
        private readonly Mock<IRegionsRepository> _regionRepoMock = new();
        private readonly Mock<ILocationsRepository> _locationRepoMock = new();
        private readonly Mock<IEventsRepository> _eventRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IIdProvider> _idProviderMock = new();
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        private readonly CreateEventFromDetectionCommandHandler _handler;

        public CreateEventFromDetectionCommandHandlerTests()
        {
            _handler = new CreateEventFromDetectionCommandHandler(
                _topicRepoMock.Object,
                _regionRepoMock.Object,
                _locationRepoMock.Object,
                _eventRepoMock.Object,
                _unitOfWorkMock.Object,
                _idProviderMock.Object,
                _mapperMock.Object,
                _dateTimeProviderMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalid_WhenTopicDoesNotExist()
        {
            // Arrange
            var cmd = new CreateEventFromDetectionCommand(
                EventFactory.CreateEventInfo(),
                "MissingTopic", "Country", "Region", "City", 10, 20);

            _topicRepoMock.Setup(x => x.GetByNameAsync(cmd.TopicName))
                          .ReturnsAsync((Topic)null);

            // Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().Contain(TopicErrors.UnExistTopic);
        }

        [Fact]
        public async Task Handle_ShouldCreateEventAndReturnSuccess_WhenAllValid()
        {
            // Arrange
            var cmd = new CreateEventFromDetectionCommand(
                EventFactory.CreateEventInfo(),
                "ValidTopic", "Country", "Region", "City", 10, 20);

            var topic = EventFactory.CreateTopic(cmd.TopicName);
            var region = EventFactory.CreateRegion(cmd.RegionName);
            var location = EventFactory.CreateLocation(cmd.CountryName, cmd.CityName, cmd.Latitude, cmd.Longitude);
            var eventId = Guid.NewGuid();
            var now = DateTime.UtcNow;

            _topicRepoMock.Setup(x => x.GetByNameAsync(cmd.TopicName))
                          .ReturnsAsync(topic);
            _regionRepoMock.Setup(x => x.GetByNameAsync(cmd.RegionName))
                           .ReturnsAsync(region);
            _locationRepoMock.Setup(x => x.GetByCoordinatesAsync(cmd.Latitude, cmd.Longitude))
                             .ReturnsAsync(location);

            _idProviderMock.Setup(x => x.GenerateId<Event>()).Returns(eventId);
            _dateTimeProviderMock.SetupGet(x => x.UtcNow).Returns(now);

            _mapperMock.Setup(m => m.Map<Event>(cmd)).Returns(new Event
            {
                Id = eventId,
                CreatedAt = now,
                LastUpdatedAt = now,
                EventInfo = cmd.EventInfo,
                Topic = topic,
                Region = region,
                Location = location
            });

            Event capturedEvent = null;
            _eventRepoMock.Setup(x => x.AddAsync(It.IsAny<Event>()))
                          .Callback<Event>(e => capturedEvent = e)
                              //.Returns((Event @event) => @event);
                              .Returns<Event>(e => Task.FromResult(e));
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.DispatchEventsAsync<Guid>())
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(eventId);

            capturedEvent.Should().NotBeNull();
            capturedEvent.Events.Should().ContainSingle(e => e is EventDetected);

            _topicRepoMock.Verify(x => x.GetByNameAsync(cmd.TopicName), Times.Once);
            _regionRepoMock.Verify(x => x.GetByNameAsync(cmd.RegionName), Times.Once);
            _locationRepoMock.Verify(x => x.GetByCoordinatesAsync(cmd.Latitude, cmd.Longitude), Times.Once);
            _eventRepoMock.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.DispatchEventsAsync<Guid>(), Times.Once);
        }
    }
}
