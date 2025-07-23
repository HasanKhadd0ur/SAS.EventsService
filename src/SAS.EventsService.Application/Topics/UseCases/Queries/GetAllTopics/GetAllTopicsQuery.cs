using Ardalis.Result;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics
{
    public record GetAllTopicsQuery : ILoggableQuery<Result<IEnumerable<TopicDTO>>>;
}
