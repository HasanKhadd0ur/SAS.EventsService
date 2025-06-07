using SAS.EventsService.Application.Common;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Entities;

namespace SAS.EventsService.Application.Regions.Common
{
    public class UserInterestDto : BaseDTO<Guid>
    {

        public Guid UserId { get; set; }
        public string InterestName { get; set; }
        public int RadiusInKm { get; set; }
        public LocationDTO Location { get; set; }
    }
}
