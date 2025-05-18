namespace SAS.EventsService.Presentation.Contracts.Events.Requests
{
    public record GetEventsByLocationRadiusRequest(
        double Latitude,
        double Longitude,
        double RadiusInKm
    );

}
