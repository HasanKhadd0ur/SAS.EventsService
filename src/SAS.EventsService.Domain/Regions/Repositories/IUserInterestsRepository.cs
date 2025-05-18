using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.Repositories;

namespace SAS.EventService.Domain.Entities

{
    public interface IUserInterestsRepository : IRepository<UserInterestRegion, Guid>
    {
    }

}