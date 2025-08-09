using Ardalis.Result;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent
{
    public record CreateEventFromDetectionCommand(
        Guid DomainId,
        EventInfo EventInfo,
        string TopicName,
        string CountryName,
        string RegionName,
        string CityName,
        double Latitude,
        double Longitude
    ) : ICommand<Result<Guid>>;
}
