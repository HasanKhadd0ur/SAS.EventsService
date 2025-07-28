using Ardalis.Result;
using FluentAssertions;
using Moq;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Events.UseCases.Commands.ChangeEventTopic;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Tests.UnitTests.Events.Application.UseCases.Commands
{
    public class ChangeEventTopicCommandHandlerTests
    {
        private readonly Mock<IEventsRepository> _eventsRepoMock = new();
        private readonly Mock<ITopicsRepository> _topicsRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly ChangeEventTopicCommandHandler _handler;

        public ChangeEventTopicCommandHandlerTests()
        {
            _handler = new ChangeEventTopicCommandHandler(_eventsRepoMock.Object, _topicsRepoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldChangeTopic_WhenEventAndTopicExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var topicId = Guid.NewGuid();
            var ev = new Event();
            var topic = new Topic { Id = topicId };

            _eventsRepoMock.Setup(r => r.GetByIdAsync(eventId, null)).ReturnsAsync(ev);
            _topicsRepoMock.Setup(r => r.GetByIdAsync(topicId, null)).ReturnsAsync(topic);

            // Act
            var result = await _handler.Handle(new ChangeEventTopicCommand(eventId, topicId), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            ev.Topic.Should().Be(topic);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalid_WhenEventNotFound()
        {
            var eventId = Guid.NewGuid();
            _eventsRepoMock.Setup(r => r.GetByIdAsync(eventId, null)).ReturnsAsync((Event)null);

            var result = await _handler.Handle(new ChangeEventTopicCommand(eventId, Guid.NewGuid()), CancellationToken.None);

            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().Contain(EventErrors.UnExistEvent);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalid_WhenTopicNotFound()
        {
            var ev = new Event();
            var topicId = Guid.NewGuid();

            _eventsRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), null)).ReturnsAsync(ev);
            _topicsRepoMock.Setup(r => r.GetByIdAsync(topicId, null)).ReturnsAsync((Topic)null);

            var result = await _handler.Handle(new ChangeEventTopicCommand(Guid.NewGuid(), topicId), CancellationToken.None);

            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().Contain(TopicErrors.UnExistTopic);
        }
    }
}
