using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Events.Common
{
    public class NamedEntityDto : BaseDTO<Guid>
    {
        public string EntityName { get; set; }
        public NamedEntityTypeDto Type { get; set; }
    }
}
