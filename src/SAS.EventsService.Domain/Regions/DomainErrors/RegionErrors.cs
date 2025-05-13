using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Regions.DomainErrors
{
    public static class RegionErrors
    {
        public static readonly DomainError InvalidRegionName =
            new("Region.InvalidRegionName", "The region name is invalid or empty.");
    }
}