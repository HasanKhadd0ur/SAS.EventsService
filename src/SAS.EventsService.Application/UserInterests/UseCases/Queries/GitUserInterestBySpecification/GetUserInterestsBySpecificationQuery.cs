using Ardalis.Result;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.UserInterests.UseCases.Queries.GitUserInterestBySpecification
{
    public record GetUserInterestsBySpecificationQuery(ISpecification<UserInterest> Specification)
        : IQuery<Result<ICollection<UserInterestDto>>>;
}
