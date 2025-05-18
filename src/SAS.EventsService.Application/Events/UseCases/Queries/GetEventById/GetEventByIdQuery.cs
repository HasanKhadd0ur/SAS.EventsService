using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventById
{
    public record GetEventByIdQuery(Guid Id) : IQuery<Result<EventDTO>>;
}
