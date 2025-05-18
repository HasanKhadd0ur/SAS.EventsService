using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using SAS.EventsService.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddMessageToEvent
{
    public class AddMessageToEventCommandHandler : ICommandHandler<AddMessageToEventCommand, Result>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AddMessageToEventCommandHandler(
            IEventsRepository eventsRepository,
            IMessagesRepository messagesRepository,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork)
        {
            _eventsRepository = eventsRepository;
            _messagesRepository = messagesRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddMessageToEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event == null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var message = _mapper.Map<Message>(request.NewMessage);
            message.EventId = @event.Id;
            message.CreatedAt = _dateTimeProvider.UtcNow;

            await _messagesRepository.AddAsync(message);

            @event.UpdateLastModifiedTime(_dateTimeProvider.UtcNow);
            
            await _eventsRepository.UpdateAsync(@event);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
