using SAS.SharedKernel.Entities;

namespace SAS.EventsService.Domain.NamedEntities.Entities
{
    public class NamedEntityType : BaseEntity<Guid>
    {
        public string TypeName { get; set; }
        public string NormalisedName { get; set; }
    }
}
