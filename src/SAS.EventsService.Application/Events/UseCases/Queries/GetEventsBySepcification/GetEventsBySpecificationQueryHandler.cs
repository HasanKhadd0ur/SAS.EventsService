using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventsBySepcification
{
    public class GetEventsBySpecificationQueryHandler
        : IQueryHandler<GetEventsBySpecificationQuery, Result<ICollection<EventDTO>>>
    {
        private readonly IEventsRepository _repository;
        private readonly IMapper _mapper;

        public GetEventsBySpecificationQueryHandler(IEventsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<EventDTO>>> Handle(GetEventsBySpecificationQuery request, CancellationToken cancellationToken)
        {
            var events = await _repository.ListAsync(request.Specification);
            var mapped = _mapper.Map<ICollection<EventDTO>>(events);
            return Result.Success(mapped);
        }
    }
}
