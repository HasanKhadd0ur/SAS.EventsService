using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.UserInterests.Repositories

{
    public interface IUserInterestsRepository : IRepository<UserInterest, Guid>
    {
        Task<IList<UserInterest>> GetNearbyUserInterests(double lat, double lon);
    }

}
