using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;
using SAS.EventsService.SharedKernel.Specification;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Events
{
    public class LocationsRepository : BaseRepository<Location, Guid>, ILocationsRepository
    {
        public LocationsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Location> GetByCoordinatesAsync(double latitude, double longitude)
        {
            var spec = new BaseSpecification<Location>(loc => loc.Latitude == latitude && loc.Longitude == longitude);
            return await FirstOrDefaultAsync(spec);
        }
    }
}