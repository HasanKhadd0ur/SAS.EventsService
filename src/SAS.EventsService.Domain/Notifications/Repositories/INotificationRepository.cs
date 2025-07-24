using SAS.EventsService.Domain.Notifications.Entitties;
using SAS.EventsService.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.Notifications.Repositories
{
    public interface INotificationRepository : IRepository<Notification, Guid>
    {
        Task<IReadOnlyList<Notification>> GetNotificationsByUserAsync(Guid userId);
        Task<Notification> GetByIdAsync(Guid id);
    }

}
