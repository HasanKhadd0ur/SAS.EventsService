using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Domain.UserInterests.Specification
{
    public class UserInterestsByUserIdSpecification : BaseSpecification<UserInterest>
    {
        public UserInterestsByUserIdSpecification(Guid userId)
            : base(ui => ui.UserId == userId)
        {
        }
    }
}

