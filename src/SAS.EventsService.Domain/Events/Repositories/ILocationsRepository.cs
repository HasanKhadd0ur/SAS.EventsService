using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.Events.Repositories
{
    public interface ILocationsRepository : IRepository<Location, Guid>
    {
        Task<Location> GetByCoordinatesAsync(double latitude, double longitude);
    }
}
