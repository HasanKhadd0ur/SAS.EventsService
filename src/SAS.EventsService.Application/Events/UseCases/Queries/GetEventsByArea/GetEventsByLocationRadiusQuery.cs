using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventsByArea
{
    public record GetEventsByLocationRadiusQuery(
            double Latitude,
            double Longitude,
            double RadiusInKm) : IQuery<Result<ICollection<EventDTO>>>;
}
