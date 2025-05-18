using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetEventsByArea
{
    public class GetEventsByLocationRadiusQueryHandler
        : IQueryHandler<GetEventsByLocationRadiusQuery, Result<ICollection<EventDTO>>>
    {
        private readonly IEventsRepository _repository;
        private readonly IMapper _mapper;
        
        public GetEventsByLocationRadiusQueryHandler(IEventsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<EventDTO>>> Handle(GetEventsByLocationRadiusQuery request, CancellationToken cancellationToken)
        {
            var spec = new EventsByLocationRadiusSpecification(request.Latitude, request.Longitude, request.RadiusInKm);
            var events = await _repository.ListAsync(spec);

            var result = _mapper.Map<ICollection<EventDTO>>(events);
            return Result.Success(result);
        }
    }
}
