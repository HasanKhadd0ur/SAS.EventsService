using Microsoft.Extensions.DependencyInjection;
using SAS.EventsService.Presentation.Mappers;

namespace SAS.EventsService.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services
                .AddMapper()
                .AddMyControllers()
                ;
            return services;
        }

        #region Configure controllers 
        private static IServiceCollection AddMyControllers(this IServiceCollection services)
        {
            services.AddControllers();
            
            //services
            //    .AddControllers();
            //.AddApplicationPart(AssemblyReference.Assembly);

            return services;
        }
        #endregion Configure controllers

        #region Mappers 
        private static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EventRequestToCommandProfile>();

            });



            return services;

        }

        #endregion Mappers 
    }
}
