using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventNamedEntities
{
    public class GetEventNamedEntitiesQueryHandler
        : IQueryHandler<GetEventNamedEntitiesQuery, Result<ICollection<NamedEntityDto>>>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public GetEventNamedEntitiesQueryHandler(IEventsRepository eventsRepository, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<NamedEntityDto>>> Handle(GetEventNamedEntitiesQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var mentions = @event.MentionedEntities;
            var dto = _mapper.Map<ICollection<NamedEntityDto>>(mentions);

            return Result.Success(dto);
        }
    }
}
