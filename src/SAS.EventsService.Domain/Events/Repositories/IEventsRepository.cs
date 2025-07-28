using SAS.EventsService.Domain.Events.Entities;
using SAS.SharedKernel.Repositories;
using SAS.SharedKernel.Utilities;

namespace SAS.EventsService.Domain.Events.Repositories
{
    public interface IEventsRepository : IRepository<Event,Guid>
    {
        Task<Event?> GetByIdWithMessagesAsync(Guid eventId, CancellationToken cancellationToken);

    }
}
