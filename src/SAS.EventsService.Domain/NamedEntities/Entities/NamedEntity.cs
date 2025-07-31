using SAS.SharedKernel.Entities;

namespace SAS.EventsService.Domain.NamedEntities.Entities
{
    public class NamedEntity : BaseEntity<Guid>
    {
        public string EntityName { get; set; }
        public Guid TypeId { get; set; }
        public NamedEntityType Type { get; set; }
        public DateTime? LastMentionedAt { get; set; }
    }

}
