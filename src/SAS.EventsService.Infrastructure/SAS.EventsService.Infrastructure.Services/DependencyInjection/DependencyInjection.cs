using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAS.EventsService.Application.Contracts.LLMs;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Infrastructure.Services;
using SAS.EventsService.Infrastructure.Services.LLMs;
using SAS.EventsService.Infrastructure.Services.Providers;

namespace SAS.EventsService.Infrastructure.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureSevices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBackgroundServices(configuration)
                .AddCronJobs()
                .AddServices(configuration);

            return services;
        }

        #region Add Servcies 
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IIdProvider, IdProvider>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services.AddHttpClient<ILLMClient, GeminiClient>();

            return services;
        }

        #endregion Add Servcies 

        #region Background jobs 
        private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        #endregion Background jobs 

        #region Cron Jobs
        private static IServiceCollection AddCronJobs(this IServiceCollection services)
        {

            return services;

        }
        #endregion
    }
}
