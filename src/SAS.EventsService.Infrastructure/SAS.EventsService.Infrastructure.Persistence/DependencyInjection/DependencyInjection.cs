using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.EventsService.Domain.Notifications.Repositories;
using SAS.EventsService.Domain.Regions.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Events;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Notifications;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Regions;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Topics;
using SAS.EventsService.Infrastructure.Persistence.UoW;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDataContext(configuration)
                .AddRepositories()
                .AddUOW();

            return services;
        }




        #region Register UOW 

        private static IServiceCollection AddUOW(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        #endregion Register UOW 

        #region Register Repositories
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<ILocationsRepository, LocationsRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IRegionsRepository, RegionsRepository>();
            services.AddScoped<IUserInterestsRepository, UserInterestsRepository>();
            services.AddScoped<INamedEntitiesRepository,NamedEntitiesRepository>();
            services.AddScoped<INamedEntityTypesRepository,NamedEntityTypesRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            return services;

        }


        #endregion Register Repositoryies

        #region Register Data context 
        private static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;

        }

        #endregion Register Data Context 
    }
}
