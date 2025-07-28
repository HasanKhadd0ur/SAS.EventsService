using Ardalis.Result;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand, Result>
    {
        private readonly IEventsRepository _eventRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventCommandHandler(IEventsRepository eventRepo, IUnitOfWork unitOfWork)
        {
            _eventRepo = eventRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var ev = await _eventRepo.GetByIdAsync(request.Id);
            if (ev is null)
                return Result.Invalid(EventErrors.UnExistEvent);

            await _eventRepo.DeleteAsync(ev);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
