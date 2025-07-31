using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Queries;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventsByNamedEntity
{
    public class GetEventsByNamedEntityIdQueryHandler
        : IQueryHandler<GetEventsByNamedEntityIdQuery, Result<ICollection<EventDTO>>>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public GetEventsByNamedEntityIdQueryHandler(
            IEventsRepository eventsRepository,
            IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<EventDTO>>> Handle(GetEventsByNamedEntityIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Event>(e => e.NamedEntityMentions.Any(ne => ne.NamedEntityId == request.NamedEntityId));

            // Include NamedEntities relationship if navigation property exists
            spec.AddInclude(e => e.NamedEntityMentions);
            spec.AddInclude(e => e.EventInfo);
            spec.AddInclude(e => e.Topic);
            spec.AddInclude(e => e.Location);
            spec.AddInclude(e => e.Region);
            spec.ApplyOptionalPagination(request.PageSize,request.PageNumber);
 
            spec.ApplyOrderByDescending(e => e.CreatedAt);
            

            var events = await _eventsRepository.ListAsync(spec);
            var result = _mapper.Map<ICollection<EventDTO>>(events);

            return Result.Success(result);
        }
    }

}
