using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetAllEvents
{
    public record GetAllEventsQuery() : IQuery<Result<ICollection<EventDTO>>>;
}
