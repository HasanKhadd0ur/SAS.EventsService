using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventInfo
{
    public class UpdateEventInfoCommandHandler : ICommandHandler<UpdateEventInfoCommand, Result>
    {
        private readonly IEventsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventInfoCommandHandler(
            IEventsRepository repository,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)    // inject unit of work
        {
            _repository = repository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateEventInfoCommand request, CancellationToken cancellationToken)
        {
            var @event = await _repository.GetByIdAsync(request.EventId);

            // 
            if (@event == null)
                return Result.Invalid(EventErrors.UnExistEvent);

            @event.UpdateEventInfo(request.NewEventInfo);

            @event.UpdateLastModifiedTime(_dateTimeProvider.UtcNow);

            await _repository.UpdateAsync(@event);
            
            // Commit all changes via UnitOfWork
            await _unitOfWork.SaveChangesAsync(cancellationToken);  



            return Result.Success();
        }
    }
}
