using Ardalis.Result;
using SAS.EventsService.Application.Topics.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Topics.UseCases.Queries.GetTopicById
{
    public record GetTopicByIdQuery(Guid Id) : IQuery<Result<TopicDTO>>;

}
