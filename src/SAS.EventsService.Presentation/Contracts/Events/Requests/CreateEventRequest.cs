using Ardalis.Result;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.CQRS.Commands;

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
