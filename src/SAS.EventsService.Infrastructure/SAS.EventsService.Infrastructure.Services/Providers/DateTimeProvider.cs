using SAS.EventsService.Application.Contracts.Providers;

namespace SAS.EventsService.Infrastructure.Services.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
