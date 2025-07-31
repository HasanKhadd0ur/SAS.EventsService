using SAS.EventsService.Application.Contracts.NER;
using SAS.EventsService.Domain.Events.DomainEvents;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.SharedKernel.DomainEvents;
using System.Threading;
using System.Threading.Tasks;

namespace SAS.EventsService.Application.Events.EventHandlers.ExtractNamedEntitiesOnEventCreated
{
    public class ExtractNamedEntitiesOnEventCreatedHandler : IDomainEventHandler<EventDetected>
    {
        private readonly INamedEntityExtractor _namedEntityExtractor;
        private readonly IEventsRepository _eventRepository;
        private readonly INamedEntitiesRepository _entityRepository;
        private readonly INamedEntityTypesRepository _typeRepository;

        public ExtractNamedEntitiesOnEventCreatedHandler(
            INamedEntityExtractor namedEntityExtractor,
            IEventsRepository eventRepository,
            INamedEntitiesRepository entityRepository,
            INamedEntityTypesRepository typeRepository)
        {
            _namedEntityExtractor = namedEntityExtractor;
            _eventRepository = eventRepository;
            _entityRepository = entityRepository;
            _typeRepository = typeRepository;
        }

        public async Task Handle(EventDetected domainEvent, CancellationToken cancellationToken)
        {
            var ev = await _eventRepository.GetByIdAsync(domainEvent.EventId);
            if (ev is null) return;

            var extractedEntities = await _namedEntityExtractor.Extract(domainEvent.Title);

            foreach (var dto in extractedEntities)
            {
                // Check or create the type
                var existingType = await _typeRepository.GetByNormalizedNameAsync(dto.Type.NormalisedName);
                if (existingType is null)
                {
                    existingType = new NamedEntityType
                    {
                        Id = Guid.NewGuid(),
                        TypeName = dto.Type.TypeName,
                        NormalisedName = dto.Type.NormalisedName
                    };
                    await _typeRepository.AddAsync(existingType);
                }

                // Check or create the named entity
                var existingEntity = await _entityRepository.GetByNameAndTypeAsync(dto.EntityName, existingType.Id);
                if (existingEntity is null)
                {
                    existingEntity = new NamedEntity
                    {
                        Id = Guid.NewGuid(),
                        EntityName = dto.EntityName,
                        TypeId = existingType.Id,
                        Type = existingType
                    };
                    await _entityRepository.AddAsync(existingEntity);
                }

                // Add mention to the event
                ev.AddNamedEntityMention(existingEntity);
            }

            await _eventRepository.UpdateAsync(ev);
        }
    }
}
