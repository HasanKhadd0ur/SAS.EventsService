using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.UserInterests.Common;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.UserInterests.UseCases.Queries.GitUserInterestBySpecification
{
    public class GetUserInterestsBySpecificationQueryHandler
        : IQueryHandler<GetUserInterestsBySpecificationQuery, Result<ICollection<UserInterestDto>>>
    {
        private readonly IUserInterestsRepository _repository;
        private readonly IMapper _mapper;

        public GetUserInterestsBySpecificationQueryHandler(IUserInterestsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<UserInterestDto>>> Handle(GetUserInterestsBySpecificationQuery request, CancellationToken cancellationToken)
        {
            var interests = await _repository.ListAsync(request.Specification);
            var dtoList = _mapper.Map<ICollection<UserInterestDto>>(interests);
            return Result.Success(dtoList);
        }
    }
}
