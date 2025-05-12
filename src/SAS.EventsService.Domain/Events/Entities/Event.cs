using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class Event : BaseEntity<Guid>
    {

        public DateTime CreatedAT { get; set; }
        public DateTime LastUpdatedAT { get; set; }
        public EventInfo EventInfo { get; set; }
        public Topic Topic { get; set; }
        public Location Location { get; set; }
        public Region Region { get; set; }
        public IEnumerable<Message> Messages { get; private set; }
    }
    public record EventInfo(
        string Title,
        string Summary,
        int SentimentScore
        );
}
