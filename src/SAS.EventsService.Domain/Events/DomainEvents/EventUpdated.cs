using SAS.SharedKernel.DomainEvents;

namespace SAS.EventsService.Domain.Events.DomainEvents
{
    // Raised when an existing event is updated (e.g., new location, topic, or region)
    public record EventUpdated(
        Guid EventId,
        string UpdatedBy,
        DateTime UpdatedAt
    ) : IDomainEvent;
}
