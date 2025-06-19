using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class NamedEntityType : BaseEntity<Guid>
    {
        public String TypeName { get; set; }
        public String NormalisedName { get; set; }
    }
}
