using Ardalis.Result;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Application.Contracts.Users;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddReviewToEvent
{
    public class AddReviewToEventCommandHandler : ICommandHandler<AddReviewToEventCommand, Result>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IRepository<Review,Guid> _reviewRepository;
        public AddReviewToEventCommandHandler(
            IEventsRepository eventsRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            ICurrentUserProvider currentUserProvider,
            IRepository<Review, Guid> reviewRepository)
        {
            _eventsRepository = eventsRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _currentUserProvider = currentUserProvider;
            _reviewRepository = reviewRepository;
        }

        public async Task<Result> Handle(AddReviewToEventCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserProvider.UserId;
            var userName = _currentUserProvider.Email;

            if (userId == null || userName == null)
            {
                return Result.Unauthorized();
            }

            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event is null)
            {
                return Result.NotFound();
            }

            var review = new Review
            {
                Id = Guid.NewGuid(),
                EventId = @event.Id,
                UserId = userId,
                UserName = userName,
                Comment = request.Comment,
                CreatedAt = _dateTimeProvider.UtcNow
            };

            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
