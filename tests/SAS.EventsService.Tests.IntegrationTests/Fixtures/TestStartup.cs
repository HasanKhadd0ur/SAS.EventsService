using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;  // Add this
using Moq;
using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Application.Mapping;
using SAS.EventsService.Application.Notifications.Common;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Regions.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Events;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Regions;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Topics;
using SAS.EventsService.Infrastructure.Persistence.UoW;
using SAS.EventsService.Infrastructure.Services;
using SAS.EventsService.Infrastructure.Services.Providers;
using SAS.SharedKernel.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAS.EventsService.Tests.IntegrationTests.Fixtures
{
    public static class TestStartup
    {
        public static IServiceProvider Initialize()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TestDb"));

            // Real Repos
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<IRegionsRepository, RegionsRepository>();
            services.AddScoped<ILocationsRepository, LocationsRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Providers
            services.AddScoped<IIdProvider, IdProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();

            // Mocked Dependencies
            var userInterestRepoMock = new Mock<IUserInterestsRepository>();
            userInterestRepoMock
                .Setup(repo => repo.GetNearbyUserInterests(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new List<UserInterest>());
            services.AddSingleton(userInterestRepoMock.Object);

            var notificationServiceMock = new Mock<INotificationService>();
            notificationServiceMock
                .Setup(n => n.NotifyUserAsync(It.IsAny<Guid>(), It.IsAny<EventNotificationDTO>()))
                .Returns(Task.CompletedTask);
            services.AddSingleton(notificationServiceMock.Object);

            // Add Logging to fix ILoggerFactory missing error
            services.AddLogging();

            // AutoMapper and MediatR
            services.AddAutoMapper(typeof(EventProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEventFromDetectionCommandHandler).Assembly));

            return services.BuildServiceProvider();
        }
    }
}
