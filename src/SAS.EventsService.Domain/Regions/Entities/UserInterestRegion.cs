using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Regions.Entities
{
    public class UserInterest :BaseEntity<Guid>
    {
        public Guid UserId { get; set; } // From Identity Server
        public string InterestName { get; set; }
        public int RadiusInKm { get; set; }
        public Location Location { get; set; }

        public void UpdateName(String newName)
        {
            this.InterestName = newName;
        }

        public void UpdateInterestArea(int newRadiusInKm)
        {
            this.RadiusInKm = newRadiusInKm;
        }
        public void UpdateLocation(Location newLocation)
        {
            this.Location = newLocation;
        }

    }
}
