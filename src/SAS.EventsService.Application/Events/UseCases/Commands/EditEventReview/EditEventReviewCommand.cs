using Ardalis.Result;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.EditReview
{
    public record EditEventReviewCommand(Guid ReviewId, string UpdatedComment) : ICommand<Result>;
    public class EditEventReviewCommandHandler : ICommandHandler<EditEventReviewCommand, Result>
    {
        private readonly IRepository<Review,Guid> _reviewsRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public EditEventReviewCommandHandler(
             IRepository<Review, Guid> reviewsRepository,
            ICurrentUserProvider currentUserProvider,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _reviewsRepository = reviewsRepository;
            _currentUserProvider = currentUserProvider;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(EditEventReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewsRepository.GetByIdAsync(request.ReviewId);
            if (review is null)
                return Result.Invalid(EventErrors.UnExistReview);

            var currentUserId = _currentUserProvider.UserId;
            if (review.UserId != currentUserId)
                return Result.Invalid(EventErrors.Forbiden);

            review.UpdateComment(request.UpdatedComment, _dateTimeProvider.UtcNow);

            await _reviewsRepository.UpdateAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
