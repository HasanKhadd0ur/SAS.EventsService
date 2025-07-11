using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.Specification;

namespace SAS.EventsService.Domain.Regions.Specifications
{
    public class UserInterestsByUserIdSpecification : BaseSpecification<UserInterest>
    {
        public UserInterestsByUserIdSpecification(Guid userId)
            : base(ui => ui.UserId == userId)
        {
        }
    }
}

