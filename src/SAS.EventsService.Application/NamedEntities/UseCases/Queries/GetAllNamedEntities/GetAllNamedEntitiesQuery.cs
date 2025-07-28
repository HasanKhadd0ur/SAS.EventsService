using Ardalis.Result;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Queries.GetAllNamedEntities
{
    public record GetAllNamedEntitiesQuery(int? PageNumber, int? PageSize)
          : IQuery<Result<ICollection<NamedEntityDto>>>;
}
