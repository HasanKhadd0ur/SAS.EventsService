using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class NamedEntityMention : BaseEntity<Guid>
    {
        public Event Event { get; set; }
        public NamedEntity NamedEntity { get; set; }
        public Guid NamedEntityId { get; set; }
        public Guid EventId { get; set; }
    }

}
