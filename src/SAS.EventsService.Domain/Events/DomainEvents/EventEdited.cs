using SAS.EventsService.SharedKernel.DomainEvents;

namespace SAS.EventsService.Domain.Events.DomainEvents
{
    // Raised when the core content (title/summary/sentiment) is edited
    public record EventEdited(
        Guid EventId,
        string OldTitle,
        string NewTitle,
        string OldSummary,
        string NewSummary
    ) : IDomainEvent;
}
