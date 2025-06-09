using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Events.Common
{
    public class MessageDTO : BaseDTO<Guid>
        {
            public string MessageId { get; set; }
            public string Content { get; set; }
            public string Source { get; set; }

            public Guid PlatformId { get; set; }
            public string Platform { get; set; }
            public DateTime CreatedAt { get; set; }
            public string SentimentLabel { get; set; }
            public string SentimentScore { get; set; }
        }
}
