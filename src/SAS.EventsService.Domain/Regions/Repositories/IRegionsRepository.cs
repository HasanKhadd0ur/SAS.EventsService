using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.Regions.Repositories


{
    public interface IRegionsRepository : IRepository<Region, Guid>
    {
        Task<Region> GetByNameAsync(string name);
    }
}
