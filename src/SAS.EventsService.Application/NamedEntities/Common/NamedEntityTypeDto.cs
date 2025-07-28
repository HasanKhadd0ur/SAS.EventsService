using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.NamedEntities.Common
{
    public class NamedEntityTypeDto : BaseDTO<Guid>
    {
        public string TypeName { get; set; }
        public string NormalisedName { get; set; }
    }
}
