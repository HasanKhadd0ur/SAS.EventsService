using Ardalis.Result;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Regions.UseCases.Queries.GetAllTopics
{
    public record GetAllInterestsQuery : IQuery<Result<IEnumerable<UserInterestDto>>>;
}
