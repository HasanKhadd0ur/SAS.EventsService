using Ardalis.Result;
using FluentAssertions;
using Moq;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Tests.UnitTests.Topics.Application.UseCases.Commands
{
    public class CreateTopicCommandHandlerTests
    {
        private readonly Mock<ITopicsRepository> _repoMock = new();
        private readonly Mock<IIdProvider> _idProviderMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly CreateTopicCommandHandler _handler;

        public CreateTopicCommandHandlerTests()
        {
            _handler = new CreateTopicCommandHandler(
                _repoMock.Object,
                _idProviderMock.Object,
                _unitOfWorkMock.Object);
        }

        public static IEnumerable<object[]> ValidTopicData =>
            new List<object[]>
            {
                new object[] { "Flood", "https://icon.com/flood.png", "Floods and disasters" },
                new object[] { "War", "https://icon.com/war.png", "Armed conflict events" },
                new object[] { "Epidemic", "", "COVID-19 and other outbreaks" },
                new object[] { "Fire", null, "Wildfires and urban fires" }
            };

        [Theory]
        [MemberData(nameof(ValidTopicData))]
        public async Task Handle_ShouldCreateTopic_GivenValidData(string name, string iconUrl, string description)
        {
            // Arrange
            var command = new CreateTopicCommand(name, iconUrl, description);
            var topicId = Guid.NewGuid();

            _idProviderMock.Setup(x => x.GenerateId<Topic>())
                           .Returns(topicId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().Be(topicId);

            _repoMock.Verify(r => r.AddAsync(It.Is<Topic>(t =>
                t.Id == topicId &&
                t.Name == name &&
                t.Description == description &&
                t.IconUrl == iconUrl
            )), Times.Once);

            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
