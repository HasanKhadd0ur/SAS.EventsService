namespace SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent
{
    public record GetEventsByLocationRadiusRequest(
        double Latitude,
        double Longitude,
        double RadiusInKm
    );

}
