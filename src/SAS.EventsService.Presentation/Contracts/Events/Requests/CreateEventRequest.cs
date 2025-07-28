using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.SharedKernel.CQRS.Commands;
using System;

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
    public record UpdateEventLocationRequest(Guid EventId, LocationDTO Location);


}
