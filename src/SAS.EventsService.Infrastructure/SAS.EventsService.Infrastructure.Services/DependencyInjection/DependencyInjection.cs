using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAS.EventsService.Application.Contracts.LLMs;
using SAS.EventsService.Application.Contracts.NER;
using SAS.EventsService.Application.Contracts.Notfications;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Infrastructure.Services.LLMs;
using SAS.EventsService.Infrastructure.Services.NER;
using SAS.EventsService.Infrastructure.Services.Notifications;
using SAS.EventsService.Infrastructure.Services.Providers;
using System.Security.Cryptography;

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
            services.AddHttpContextAccessor();

            services.AddSingleton<IIdProvider, IdProvider>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

            services.AddHttpClient<ILLMClient, GeminiClient>();
            services.AddSingleton<INotificationService, SignalRNotificationService>();

            services.AddSignalR();

            services.Configure<NERServiceSettings>(configuration.GetSection("NERService"));

            // Register HttpNERExtractor with HttpClient factory reading BaseUrl from config
            services.AddHttpClient<INamedEntityExtractor, HttpNERExtractor>((sp, client) =>
            {
                var nerSettings = sp.GetRequiredService<IOptions<NERServiceSettings>>().Value;
                client.BaseAddress = new Uri(nerSettings.BaseUrl);
            });


            return services;
        }

        #endregion Add Servcies 
        #region Add Auth 
        public static IServiceCollection AddMyCustomAuth(IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("JwtSettings:Issuer");
            var audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("JwtSettings:Audience");
            var publicKeyBase64 = configuration["JwtSettings:PublicKeyPath"] ?? throw new ArgumentNullException("JwtSettings:PublicKeyPath");

            var publicKey = LoadRsaPublicKey(publicKeyBase64);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        ValidateAudience = true,
                        ValidAudience = audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = publicKey,

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/notifications"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
        #endregion Add Auth
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
        private static RsaSecurityKey LoadRsaPublicKey(string base64Key)
        {
            var rsa = RSA.Create();
            var keyBytes = Convert.FromBase64String(base64Key);
            rsa.ImportSubjectPublicKeyInfo(keyBytes, out _);
            return new RsaSecurityKey(rsa);
        }
    }
}
