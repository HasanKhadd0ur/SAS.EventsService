using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Regions.DomainErrors
{
    public static class RegionErrors
    {
        public static readonly DomainError InvalidRegionName =
            new("Region.InvalidRegionName", "The region name is invalid or empty.");
    }
    public static class UserInterestErrors
    {
        public static readonly DomainError UserInterestUnExist =
            new("UserInterest.UserInterestUnExist", "The Interest is un exist");
    }
}