using Ardalis.Result;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventNamedEntities
{
    public record GetEventNamedEntitiesQuery(Guid EventId)
          : IQuery<Result<ICollection<NamedEntityDto>>>;
}
