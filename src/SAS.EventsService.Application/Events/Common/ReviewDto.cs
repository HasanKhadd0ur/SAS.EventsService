using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Events.Common
{
    public class ReviewDto : BaseDTO<Guid>
        {
            public Guid EventId { get; set; }
        
            public Guid UserId { get; set; }
            public string UserName { get; set; }

            public string Comment { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime LastUpdatedAt { get; set; }
}


}

