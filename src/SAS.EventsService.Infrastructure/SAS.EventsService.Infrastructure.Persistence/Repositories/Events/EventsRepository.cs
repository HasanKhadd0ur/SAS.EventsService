using Microsoft.EntityFrameworkCore;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;
using SAS.EventsService.SharedKernel.Utilities;
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
        
        public async Task<Event?> GetByIdWithMessagesAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var spec = new EventWithMessagesByIdSpecification(eventId);
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);

        }
    }

}