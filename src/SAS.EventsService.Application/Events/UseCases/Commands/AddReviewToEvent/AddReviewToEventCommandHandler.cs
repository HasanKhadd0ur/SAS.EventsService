using Ardalis.Result;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddReviewToEvent
{
    public class AddReviewToEventCommandHandler : ICommandHandler<AddReviewToEventCommand, Result>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AddReviewToEventCommandHandler(
            IEventsRepository eventsRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _eventsRepository = eventsRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(AddReviewToEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            if (string.IsNullOrWhiteSpace(request.Comment))
                return Result.Invalid(EventErrors.EmptyComment);

            @event.AddReview(request.UserId, request.UserName, request.Comment);
            @event.UpdateLastModifiedTime(_dateTimeProvider.UtcNow);

            await _eventsRepository.UpdateAsync(@event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
