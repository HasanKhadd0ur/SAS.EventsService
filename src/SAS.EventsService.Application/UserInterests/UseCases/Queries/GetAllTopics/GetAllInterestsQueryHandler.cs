using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Specification;

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
            var spec = new BaseSpecification<UserInterest>();
            spec.AddInclude(e => e.Location);
            
            var interests = await _repo.ListAsync(spec);
            var dtoList = _mapper.Map<IEnumerable<UserInterestDto>>(interests);
            return Result.Success(dtoList);
        }
    }
}
