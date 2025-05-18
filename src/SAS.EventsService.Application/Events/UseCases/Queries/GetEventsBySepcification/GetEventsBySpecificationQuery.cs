using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventsBySepcification
{
    public record GetEventsBySpecificationQuery(ISpecification<Event> Specification)
        : IQuery<Result<ICollection<EventDTO>>>;
}
