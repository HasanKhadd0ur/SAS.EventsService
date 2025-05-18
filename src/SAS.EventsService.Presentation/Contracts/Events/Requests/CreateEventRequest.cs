using SAS.EventsService.Domain.Events.ValueObjects;

namespace SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent
{
    public record CreateEventRequest(
        EventInfo EventInfo,
        string TopicName,
        string CountryName,
        string RegionName,
        string CityName,
        double Latitude,
        double Longitude
    );

}
