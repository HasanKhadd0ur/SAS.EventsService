using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetAllEvents
{
    public record GetAllEventsQuery(
        int? PageNumber,
        int? PageSize) : IQuery<Result<ICollection<EventDTO>>>;
}
