using Ardalis.Result;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Regions.UseCases.Queries.GetTopicById
{
    public record GetUserInterestByIdQuery(Guid Id) : IQuery<Result<UserInterestDto>>;

}
