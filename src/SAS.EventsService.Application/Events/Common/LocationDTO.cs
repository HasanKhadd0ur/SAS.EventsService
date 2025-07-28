using SAS.EventsService.Application.Common;

namespace SAS.EventsService.Application.Events.Common
{
    public class LocationDTO : BaseDTO<Guid>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }
}
