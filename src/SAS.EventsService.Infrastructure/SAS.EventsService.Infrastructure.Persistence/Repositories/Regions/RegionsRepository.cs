using SAS.EventsService.Domain.Regions.Entities;
using SAS.EventsService.Domain.Regions.Repositories;
using SAS.EventsService.Domain.Regions.Specifications;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Regions
{
    public class RegionsRepository : BaseRepository<Region, Guid>, IRegionsRepository
    {
        public RegionsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Region> GetByNameAsync(string name)
        {
            var spec = new RegionByNameSpecification(name);
            return await FirstOrDefaultAsync(spec);
        }
    }
}
