using Ardalis.Result;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.MarkEventAsReviewed
{
    public class MarkEventAsReviewedCommandHandler : ICommandHandler<MarkEventAsReviewedCommand, Result>
    {
        private readonly IEventsRepository _eventRepo;
        private readonly IUnitOfWork _unitOfWork;

        public MarkEventAsReviewedCommandHandler(IEventsRepository eventRepo, IUnitOfWork unitOfWork)
        {
            _eventRepo = eventRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(MarkEventAsReviewedCommand request, CancellationToken cancellationToken)
        {
            var ev = await _eventRepo.GetByIdAsync(request.EventId);
            if (ev is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            ev.MarkAsReviewed();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
