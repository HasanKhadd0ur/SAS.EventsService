using Ardalis.Result;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent
{
    public record CreateEventFromDetectionCommand(
        EventInfo EventInfo,
        string TopicName,
        string CountryName,
        string RegionName,
        string CityName,
        double Latitude,
        double Longitude
    ) : ICommand<Result<Guid>>;
}
