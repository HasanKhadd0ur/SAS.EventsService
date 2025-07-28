
using Microsoft.OpenApi.Models;


namespace SAS.EventsService.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddApiSwagger()
                .AddApiCors(configuration)
                .AddMyMiddlewares()
                .AddLogging(configuration)
                ;


            services.AddOutputCache();

            return services;
        }


        #region Api Docs Swagger
        private static IServiceCollection AddApiSwagger(this IServiceCollection services)
        {


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SAS.Api", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
        #endregion  Api Docs Swagger

        #region Cors
        private static IServiceCollection AddApiCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll", policy =>
            //    {
            //        policy
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader();
            //    });
            //});

            return services;
        }

        #endregion Cors
        #region Loggging 
        private static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            
            return services;
        }

        #endregion Logging
        #region Middlewares 

        private static IServiceCollection AddMyMiddlewares(this IServiceCollection services)
        {

            return services;
        }
        #endregion Middlewares 


    }


}

