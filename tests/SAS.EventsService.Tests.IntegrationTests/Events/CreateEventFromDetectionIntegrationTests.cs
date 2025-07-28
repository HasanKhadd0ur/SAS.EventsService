using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Regions.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.SharedKernel.Utilities;
using SAS.EventsService.Tests.IntegrationTests.Events;
using SAS.EventsService.Tests.IntegrationTests.Fixtures;
using Xunit;

namespace SAS.EventsService.Tests.IntegrationTests.EventTests
{
    public class CreateEventFromDetectionTests : IClassFixture<TestFixture>
    {
        private readonly IServiceProvider _services;
        private readonly AppDbContext _db;

        public CreateEventFromDetectionTests(TestFixture fixture)
        {
            _services = fixture.Services;
            _db = fixture.DbContext;
        }

        [Fact]
        public async Task Should_CreateEvent_WhenDataValid()
        {
            // Arrange
            var handler = new CreateEventFromDetectionCommandHandler(
                _services.GetRequiredService<ITopicsRepository>(),
                _services.GetRequiredService<IRegionsRepository>(),
                _services.GetRequiredService<ILocationsRepository>(),
                _services.GetRequiredService<IEventsRepository>(),
                _services.GetRequiredService<IUnitOfWork>(),
                _services.GetRequiredService<IIdProvider>(),
                _services.GetRequiredService<IMapper>(),
                _services.GetRequiredService<IDateTimeProvider>());

            var command = new CreateEventFromDetectionCommand(
                new SAS.EventsService.Domain.Events.ValueObjects.EventInfo("Title", "Summary", 0.9, "Neutral"),
                "ValidTopic",
                "CountryName",
                "RegionName",
                "CityName",
                33.5,
                36.3);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            var created = await _db.Events
                .Include(e => e.Topic)
                .Include(e => e.Region)
                .Include(e => e.Location)
                .FirstOrDefaultAsync(e => e.Id == result.Value);

            created.Should().NotBeNull();
            created.Topic.Name.Should().Be("ValidTopic");
            created.Region.Name.Should().Be("RegionName");
        }
    }
}
