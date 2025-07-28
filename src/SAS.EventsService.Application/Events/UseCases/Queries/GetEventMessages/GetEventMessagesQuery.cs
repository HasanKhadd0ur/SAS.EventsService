using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetAllEvents
{
    public record GetEventMessagesQuery(
        Guid EventId,
        int? PageNumber =null,
        int? PageSize=null
        ) : IQuery<Result<ICollection<MessageDto>>>;
}
