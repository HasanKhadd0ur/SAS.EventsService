using Microsoft.EntityFrameworkCore;
using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.EventsService.Domain.UserInterests.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Regions
{
    public class UserInterestsRepository : BaseRepository<UserInterest, Guid>, IUserInterestsRepository
    {
        public UserInterestsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IList<UserInterest>> GetNearbyUserInterests(double lat, double lon)
        {
            // Haversine formula in raw SQL with JOIN to Location
            var sql = @"
                    SELECT ui.*
                    FROM UserInterests ui
                    INNER JOIN Locations l ON ui.LocationId = l.Id
                    WHERE (
                        6371 * acos(
                            cos(radians({0})) * cos(radians(l.Latitude)) *
                            cos(radians(l.Longitude) - radians({1})) +
                            sin(radians({0})) * sin(radians(l.Latitude))
                        )
                    ) <= ui.RadiusInKm";

            return await _dbContext.UserInterests
                .FromSqlRaw(sql, lat, lon)
                .ToListAsync();
        }


     }

}
