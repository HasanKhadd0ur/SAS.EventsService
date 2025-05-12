using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Events
{
    public class EventsRepository : BaseRepository<Event, Guid>, IEventsRepository
    {

        public EventsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
