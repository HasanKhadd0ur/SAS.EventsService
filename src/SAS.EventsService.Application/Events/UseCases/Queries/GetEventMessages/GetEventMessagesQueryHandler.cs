using Ardalis.Result;
using AutoMapper;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetAllEvents
{
    public class GetEventMessagesQueryHandler : IQueryHandler<GetEventMessagesQuery, Result<ICollection<MessageDto>>>
    {
        private readonly IEventsRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetEventMessagesQueryHandler(IEventsRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<MessageDto>>> Handle(GetEventMessagesQuery request, CancellationToken cancellationToken)
        {
            // Fetch the event with messages included
            var eventEntity = await _eventRepository.GetByIdWithMessagesAsync(request.EventId, cancellationToken);

            if (eventEntity == null)
                return Result.Invalid(EventErrors.UnExistEvent);

            var messagesDto = _mapper.Map<ICollection<MessageDto>>(eventEntity.Messages);
            return Result.Success(messagesDto);
        }
    }
}
