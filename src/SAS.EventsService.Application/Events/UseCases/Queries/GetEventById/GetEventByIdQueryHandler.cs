using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Specification;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventById
{
    public class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, Result<EventDTO>>
    {
        private readonly IEventsRepository _eventRepo;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IEventsRepository eventsRepo, IMapper mapper)
        {
            _eventRepo = eventsRepo;
            _mapper = mapper;
        }

        public async Task<Result<EventDTO>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new BaseSpecification<Event>();
            spec.AddInclude(e=> e.Location);
            spec.AddInclude(e => e.Topic);
            
            var @event = await _eventRepo.GetByIdAsync(request.Id,spec);

            if (@event is null) return Result.Invalid(EventErrors.UnExistEvent);

            return Result.Success(_mapper.Map<EventDTO>(@event));
        }
    }


}
