using Microsoft.EntityFrameworkCore;
using SAS.EventsService.Domain.Notifications.Entitties;
using SAS.EventsService.Domain.Notifications.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Notifications
{
    public class NotificationRepository : BaseRepository<Notification, Guid>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Notification>> GetNotificationsByUserAsync(Guid userId)
        {
            return await _dbContext.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Notifications
                .FirstOrDefaultAsync(n => n.Id == id);
        }
    }

}
