using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetAllEvents
{
    public class GetAllEventsQueryHandler : IQueryHandler<GetAllEventsQuery, Result<ICollection<EventDTO>>>
    {
        private readonly IEventsRepository _eventRepo;
        private readonly IMapper _mapper;
        private readonly BaseEventSpecification _eventSpecification;
        public GetAllEventsQueryHandler(IEventsRepository eventRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
            _eventSpecification = new BaseEventSpecification();
        }

        public async Task<Result<ICollection<EventDTO>>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            _eventSpecification.ApplyOptionalPagination(request.PageSize, request.PageNumber);
            _eventSpecification.AddInclude(e => e.Topic);
            var events = await _eventRepo.ListAsync(_eventSpecification);
            return Result.Success(_mapper.Map<ICollection<EventDTO>>(events));
        }
    }

}
