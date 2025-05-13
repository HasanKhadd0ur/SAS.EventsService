using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MediatR;
using System.Reflection;
using SAS.EventsService.Application.Behaviors.ValidationBehavior;
using SAS.EventsService.Application.Behaviors.LoggingBehavior;
using SAS.EventsService.Application.Mapping;
using FluentValidation;

namespace SAS.EventsService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMyMediatR()
                .AddMappers();



            return services;
        }

        #region Mediator
        private static IServiceCollection AddMyMediatR(this IServiceCollection services)
        {
            // Registers MediatR handlers from this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Registers pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Registers FluentValidation validators from the current assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
        #endregion Mediator


        #region Mappers 
        private static IServiceCollection AddMappers(this IServiceCollection services)
        {

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EventProfile>();
            });


            return services;
        }

        #endregion Mappers

    }
}

