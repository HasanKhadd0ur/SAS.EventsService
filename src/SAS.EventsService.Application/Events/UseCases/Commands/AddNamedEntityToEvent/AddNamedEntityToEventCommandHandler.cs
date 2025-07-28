using Ardalis.Result;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.NamedEntities.DomainErrors;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddNamedEntityToEvent
{
    public class AddNamedEntityToEventCommandHandler : ICommandHandler<AddNamedEntityToEventCommand, Result>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly INamedEntitiesRepository _namedEntitiesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddNamedEntityToEventCommandHandler(
            IEventsRepository eventsRepository,
            INamedEntitiesRepository namedEntitiesRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _eventsRepository = eventsRepository;
            _namedEntitiesRepository = namedEntitiesRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(AddNamedEntityToEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var namedEntity = await _namedEntitiesRepository.GetByIdAsync(request.NamedEntityId);
            if (namedEntity is null)
                return Result.Invalid(NamedEntityErrors.UnExistNamedEntity);

            @event.AddNamedEntityMention(namedEntity);
            @event.UpdateLastModifiedTime(_dateTimeProvider.UtcNow);

            await _eventsRepository.UpdateAsync(@event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
