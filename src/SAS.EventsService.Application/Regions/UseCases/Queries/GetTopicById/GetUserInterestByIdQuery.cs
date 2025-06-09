using Ardalis.Result;
using SAS.EventsService.Application.Regions.Common;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Regions.UseCases.Queries.GetTopicById
{
    public record GetUserInterestByIdQuery(Guid Id) : IQuery<Result<UserInterestDto>>;

}
