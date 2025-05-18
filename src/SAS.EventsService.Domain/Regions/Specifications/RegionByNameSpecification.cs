using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.Specification;

namespace SAS.EventsService.Domain.Regions.Specifications
{
    public class RegionByNameSpecification : BaseSpecification<Region>
    {
        public RegionByNameSpecification(string name)
            : base(region => region.Name == name)
        {
        }
    }
}

