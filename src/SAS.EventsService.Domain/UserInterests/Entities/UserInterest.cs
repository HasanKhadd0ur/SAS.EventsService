using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.UserInterests.Entities
{
    public class UserInterest :BaseEntity<Guid>
    {
        public Guid UserId { get; set; } // From Identity Server
        public string InterestName { get; set; }
        public int RadiusInKm { get; set; }
        public Location Location { get; set; }

        public void UpdateName(string newName)
        {
            InterestName = newName;
        }

        public void UpdateInterestArea(int newRadiusInKm)
        {
            RadiusInKm = newRadiusInKm;
        }
        public void UpdateLocation(Location newLocation)
        {
            Location = newLocation;
        }

    }
}
