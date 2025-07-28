using SAS.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.UserInterests.DomainErrors
{
    public static class UserInterestErrors
    {
        public static readonly DomainError UserInterestUnExist =
            new("UserInterest.UserInterestUnExist", "The Interest is un exist");
    }
}
