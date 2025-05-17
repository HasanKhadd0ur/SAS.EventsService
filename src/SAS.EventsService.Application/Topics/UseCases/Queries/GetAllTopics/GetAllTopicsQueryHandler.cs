using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Topics.UseCases.Queries.GetAllTopics
{
    public class GetAllTopicsQueryHandler : IQueryHandler<GetAllTopicsQuery, Result<IEnumerable<TopicDTO>>>
    {
        private readonly ITopicsRepository _repo;
        private readonly IMapper _mapper;

        public GetAllTopicsQueryHandler(ITopicsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TopicDTO>>> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
        {
            var topics = await _repo.ListAsync();
            var dtoList = _mapper.Map<IEnumerable<TopicDTO>>(topics);
            return Result.Success(dtoList);
        }
    }
}
