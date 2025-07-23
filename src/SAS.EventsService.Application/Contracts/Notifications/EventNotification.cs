namespace SAS.EventsService.Application.Contracts.Notfications
{
    public class EventNotification
    {
        public Guid EventId { get; init; }
        public string Title { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public DateTime OccurredAt { get; init; }
    }

}
