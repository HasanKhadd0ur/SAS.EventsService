using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetTopicById;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Regions.UseCases.Queries.GetTopicById
{
    public class GetUserInterestByIdQueryHandler : IQueryHandler<GetUserInterestByIdQuery, Result<UserInterestDto>>
    {
        private readonly IUserInterestsRepository _repo;
        private readonly IMapper _mapper;

        public GetUserInterestByIdQueryHandler(IUserInterestsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<UserInterestDto>> Handle(GetUserInterestByIdQuery request, CancellationToken cancellationToken)
        {
            var topic = await _repo.GetByIdAsync(request.Id);

            if (topic is null) return Result.Invalid(TopicErrors.UnExistTopic);

            return Result.Success(_mapper.Map<UserInterestDto>(topic));
        }
    }

}
