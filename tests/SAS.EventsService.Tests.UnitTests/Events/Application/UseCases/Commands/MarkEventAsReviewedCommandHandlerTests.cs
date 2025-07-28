using Ardalis.Result;
using FluentAssertions;
using Moq;
using SAS.EventsService.Application.Events.UseCases.Commands.MarkEventAsReviewed;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Tests.UnitTests.Events.Application.UseCases.Commands
{
    public class MarkEventAsReviewedCommandHandlerTests
    {
        private readonly Mock<IEventsRepository> _repoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly MarkEventAsReviewedCommandHandler _handler;

        public MarkEventAsReviewedCommandHandlerTests()
        {
            _handler = new MarkEventAsReviewedCommandHandler(_repoMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenEventExists()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var ev = new Event();
            _repoMock.Setup(r => r.GetByIdAsync(eventId, null)).ReturnsAsync(ev);

            // Act
            var result = await _handler.Handle(new MarkEventAsReviewedCommand(eventId), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            ev.IsReviewed.Should().BeTrue();
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnInvalid_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(eventId, null)).ReturnsAsync((Event)null);

            // Act
            var result = await _handler.Handle(new MarkEventAsReviewedCommand(eventId), CancellationToken.None);

            // Assert
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().Contain(EventErrors.UnExistEvent);
        }
    }
}
