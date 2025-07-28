using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Commands;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Application.Events.UseCases.Commands.BulkAddMessagesToEvent
{
    public class BulkAddMessagesToEventCommandHandler : ICommandHandler<BulkAddMessagesToEventCommand, Result>
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdProvider _idProvider;

        public BulkAddMessagesToEventCommandHandler(
            IEventsRepository eventsRepository,
            IMessagesRepository messagesRepository,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork, 
            IIdProvider idProvider)
        {
            _eventsRepository = eventsRepository;
            _messagesRepository = messagesRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
            _unitOfWork = unitOfWork;
            _idProvider = idProvider;
        }

        public async Task<Result> Handle(BulkAddMessagesToEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await _eventsRepository.GetByIdAsync(request.EventId);
            if (@event == null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var messages = _mapper.Map<List<Message>>(request.NewMessages);

            foreach (var message in messages)
            {
                message.EventId = @event.Id;
                message.Id = _idProvider.GenerateId<Message>();

            }

            await _messagesRepository.AddRangeAsync(messages); // assumes this method exists

            @event.UpdateLastModifiedTime(_dateTimeProvider.UtcNow);
            await _eventsRepository.UpdateAsync(@event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
