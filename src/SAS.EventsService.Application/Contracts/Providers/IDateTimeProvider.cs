
namespace SAS.EventsService.Application.Contracts.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }

}
