using SAS.EventsService.Application.Common;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.EventsService.Application.Topics.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.SharedKernel.Entities;

namespace SAS.EventsService.Application.Events.Common
{
    public class EventDTO : BaseDTO<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public EventInfo EventInfo { get; set; }
        public TopicDTO Topic { get; set; }
        public LocationDTO Location { get; set; }
        public Boolean IsReviewed { get; set; }
        public IEnumerable<MessageDto> Messages { get; set; }
        public IEnumerable<NamedEntityDto> MentionedEntities { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }

    }


}

