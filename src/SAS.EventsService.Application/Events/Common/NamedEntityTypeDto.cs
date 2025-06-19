using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Events.Common
{
    public class NamedEntityTypeDto : BaseDTO<Guid>
    {
        public String TypeName { get; set; }
        public String NormalisedName { get; set; }
    }
}
