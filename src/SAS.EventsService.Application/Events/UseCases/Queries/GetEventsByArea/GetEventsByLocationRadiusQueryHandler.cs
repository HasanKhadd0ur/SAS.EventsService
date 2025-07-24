using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;

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

            events= events
                .Where(e =>
                    e.Location != null &&
                    GetDistanceInKm(
                        request.Latitude,
                        request.Longitude,
                        e.Location.Latitude,
                        e.Location.Longitude) <= request.RadiusInKm
                )
                .ToList();



            var result = _mapper.Map<ICollection<EventDTO>>(events);
            return Result.Success(result);
        }
        private static double GetDistanceInKm(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Earth radius in kilometers
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double DegreesToRadians(double deg) => deg * (Math.PI / 180);
    }
}
