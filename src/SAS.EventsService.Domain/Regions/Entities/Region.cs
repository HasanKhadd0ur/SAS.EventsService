using SAS.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Regions.Entities
{
    public class Region : BaseEntity<Guid>
    {
        public string Name { get; set; }
        //public ICollection<UserInterestRegion> UserInterests { get; set; }
    }
}
