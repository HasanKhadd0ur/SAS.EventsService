using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class Message : BaseEntity<Guid>
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public string MessageId { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }

        public Guid PlatformId { get; set; }
        public string Platform { get; set; }

        public DateTime CreatedAt { get; set; }
        public string SentimentLabel { get; set; }
        public string SentimentScore { get; set; }
    }
}
