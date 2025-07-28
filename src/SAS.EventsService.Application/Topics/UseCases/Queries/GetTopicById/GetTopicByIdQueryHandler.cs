using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Topics.UseCases.Queries.GetTopicById
{
    public class GetTopicByIdQueryHandler : IQueryHandler<GetTopicByIdQuery, Result<TopicDTO>>
    {
        private readonly ITopicsRepository _repo;
        private readonly IMapper _mapper;

        public GetTopicByIdQueryHandler(ITopicsRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<TopicDTO>> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            var topic = await _repo.GetByIdAsync(request.Id);

            if (topic is null) return Result.Invalid(TopicErrors.UnExistTopic);

            return Result.Success(_mapper.Map<TopicDTO>(topic));
        }
    }

}
