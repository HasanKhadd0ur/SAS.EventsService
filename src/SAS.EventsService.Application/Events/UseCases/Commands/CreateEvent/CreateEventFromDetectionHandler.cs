using AutoMapper;
using Ardalis.Result;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.Domain.Regions.Repositories;
using SAS.EventsService.SharedKernel.Utilities;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Regions.Entities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent
{
    public class CreateEventFromDetectionHandler : ICommandHandler<CreateEventFromDetectionCommand, Result<Guid>>
    {
        private readonly ITopicsRepository _topicRepo;
        private readonly IRegionsRepository _regionRepo;
        private readonly ILocationsRepository _locationRepo;
        private readonly IEventsRepository _eventRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdProvider _idProvider;
        private readonly IMapper _mapper;

        public CreateEventFromDetectionHandler(
            ITopicsRepository topicRepo,
            IRegionsRepository regionRepo,
            ILocationsRepository locationRepo,
            IEventsRepository eventRepo,
            IUnitOfWork unitOfWork,
            IIdProvider idProvider,
            IMapper mapper)
        {
            _topicRepo = topicRepo;
            _regionRepo = regionRepo;
            _locationRepo = locationRepo;
            _eventRepo = eventRepo;
            _unitOfWork = unitOfWork;
            _idProvider = idProvider;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateEventFromDetectionCommand request, CancellationToken cancellationToken)
        {
            // Retrieve topic by name
            var topic = await _topicRepo.GetByNameAsync(request.TopicName);
            if (topic is null) return Result.Invalid(TopicErrors.UnExistTopic);

            // Retrieve region by country and city names (which could be hierarchical now)
            var region = await _regionRepo.GetByNameAsync(request.RegionName);
            if (region is null)
            {
                // If region doesn't exist, we can either throw an error or create it
                var regionId = _idProvider.GenerateId<string>(); // Generate a new ID for the region
                region = new Region { Id=regionId,Name= request.RegionName };
                await _regionRepo.AddAsync(region);
            }

            // Retrieve or create location
            var location = await _locationRepo.GetByCoordinatesAsync(request.Latitude, request.Longitude);
            if (location is null)
            {
                var locationId = _idProvider.GenerateId<Guid>();
                location = _mapper.Map<Location>(request);
                location.Id = locationId;
                await _locationRepo.AddAsync(location);
            }

            
            // Create event with mapped data
            var eventId = _idProvider.GenerateId<Guid>();

            // Map the CreateEventFromDetectionCommand to the Event entity using AutoMapper
            var @event = _mapper.Map<Event>(request);

            @event.EventInfo = request.EventInfo; // Ensure EventInfo is set from the request

            @event.Id = eventId;
            

            // Save event to repository
            await _eventRepo.AddAsync(@event);
            await _unitOfWork.SaveChangesAsync();

            // Dispatch domain events
            await _unitOfWork.DispatchEventsAsync<Guid>();

            return Result.Success(@event.Id);
        }
    }
}
