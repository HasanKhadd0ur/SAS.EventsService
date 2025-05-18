using SAS.EventsService.Application.Common;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Domain.Events.ValueObjects;

namespace SAS.EventsService.Application.Events.Common
{
    public class EventDTO : BaseDTO<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public EventInfo EventInfo { get; set; }
        public TopicDTO Topic { get; set; }
        public LocationDTO Location { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
