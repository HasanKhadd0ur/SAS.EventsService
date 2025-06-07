using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Topics.Common
{
    public class TopicDTO : BaseDTO<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
