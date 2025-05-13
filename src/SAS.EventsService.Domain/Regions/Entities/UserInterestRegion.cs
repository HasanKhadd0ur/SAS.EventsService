using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Regions.Entities
{
    public class UserInterestRegion :BaseEntity<Guid>
    {
        public Guid UserId { get; set; } // From Identity Server
        public Guid? RegionId { get; set; }
        public Region Region { get; set; }

    }
}
