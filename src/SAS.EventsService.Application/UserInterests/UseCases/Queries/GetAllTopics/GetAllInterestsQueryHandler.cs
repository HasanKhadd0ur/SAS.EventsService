using Ardalis.Result;
using AutoMapper;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Regions.UseCases.Queries.GetAllTopics

{
    public class GetAllInterestsQueryHandler : IQueryHandler<GetAllInterestsQuery, Result<IEnumerable<UserInterestDto>>>
    {
        private readonly IUserInterestsRepository _repo;
        private readonly IMapper _mapper;

        public GetAllInterestsQueryHandler(IUserInterestsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserInterestDto>>> Handle(GetAllInterestsQuery request, CancellationToken cancellationToken)
        {
            var interests = await _repo.ListAsync();
            var dtoList = _mapper.Map<IEnumerable<UserInterestDto>>(interests);
            return Result.Success(dtoList);
        }
    }
}
