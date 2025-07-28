using AutoMapper;
using FluentAssertions;
using Moq;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Application.Events.UseCases.Queries.GetEventsBySepcification;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Tests.UnitTests.Events.Application.UseCases.Queries
{
    public class GetEventsBySpecificationQueryHandlerTests
    {
        private readonly Mock<IEventsRepository> _eventRepoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly GetEventsBySpecificationQueryHandler _handler;

        public GetEventsBySpecificationQueryHandlerTests()
        {
            _handler = new GetEventsBySpecificationQueryHandler(
                _eventRepoMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedEvents_WhenSpecificationIsProvided()
        {
            // Arrange
            var spec = new BaseSpecification<Event>(); // could add criteria if needed
            var query = new GetEventsBySpecificationQuery(spec);

            var domainEvents = new List<Event>
            {
                new Event { Id = Guid.NewGuid() },
                new Event { Id = Guid.NewGuid() }
            };

            var mappedDtos = new List<EventDTO>
            {
                new EventDTO { Id = domainEvents[0].Id },
                new EventDTO { Id = domainEvents[1].Id }
            };

            _eventRepoMock.Setup(r => r.ListAsync(spec))
                .ReturnsAsync(domainEvents);

            _mapperMock.Setup(m => m.Map<ICollection<EventDTO>>(domainEvents))
                .Returns(mappedDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Status.Should().Be(Ardalis.Result.ResultStatus.Ok);
            result.Value.Should().BeEquivalentTo(mappedDtos);

            _eventRepoMock.Verify(r => r.ListAsync(spec), Times.Once);
            _mapperMock.Verify(m => m.Map<ICollection<EventDTO>>(domainEvents), Times.Once);
        }
    }
}
