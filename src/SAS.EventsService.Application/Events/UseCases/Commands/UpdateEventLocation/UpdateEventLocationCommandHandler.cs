using Ardalis.Result;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventLocation
{
    public class UpdateEventLocationCommandHandler : ICommandHandler<UpdateEventLocationCommand, Result>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventLocationCommandHandler(
            IEventsRepository eventsRepository,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)
        {
            _eventsRepository = eventsRepository;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateEventLocationCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var newLocation = new Location {
                Latitude = request.Location.Latitude,
                Longitude = request.Location.Longitude,
                Country = request.Location.Country,
                City = request.Location.City
            };

            @event.UpdateLocation(newLocation);
            @event.UpdateLastModifiedTime(_dateTimeProvider.UtcNow);

            await _eventsRepository.UpdateAsync(@event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
