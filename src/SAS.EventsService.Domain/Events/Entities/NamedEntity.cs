using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class NamedEntity : BaseEntity<Guid>
    {
        public String EntityName { get; set; }
        public Guid TypeId { get; set; }
        public NamedEntityType Type { get; set; }
    }

}
