using SAS.SharedKernel.DomainEvents;

namespace SAS.EventsService.Domain.Events.DomainEvents
{
    // Raised when a new event is detected/created (e.g., by the detection system)
    public record EventDetected(
        Guid EventId,
        string Title,
        string RegionName,
        DateTime CreatedAt,
        double Latitude,
        double Longitude
    ) : IDomainEvent;
}
