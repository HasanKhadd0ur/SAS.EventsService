using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Events
{
    public class MessagesRepository : BaseRepository<Message, Guid>, IMessagesRepository
    {

        public MessagesRepository(AppDbContext context) : base(context)
        {
        }
    }
}