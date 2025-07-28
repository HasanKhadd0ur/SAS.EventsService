using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.NamedEntities.Common
{
    public class NamedEntityDto : BaseDTO<Guid>
    {
        public string EntityName { get; set; }
        public NamedEntityTypeDto Type { get; set; }
    }
}
