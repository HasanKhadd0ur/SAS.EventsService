using SAS.EventsService.SharedKernel.Entities;

namespace SAS.EventsService.Domain.Events.Entities
{
    public class Location : BaseEntity<Guid>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }
}
