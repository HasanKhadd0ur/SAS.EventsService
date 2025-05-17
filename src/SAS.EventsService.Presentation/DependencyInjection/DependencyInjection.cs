
using Microsoft.Extensions.DependencyInjection;

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

            services
                .AddControllers();
                //.AddApplicationPart(AssemblyReference.Assembly);

            return services;
        }
        #endregion Configure controllers

        #region Mappers 
        private static IServiceCollection AddMapper(this IServiceCollection services)
        {


            return services;

        }

        #endregion Mappers 
    }
}
