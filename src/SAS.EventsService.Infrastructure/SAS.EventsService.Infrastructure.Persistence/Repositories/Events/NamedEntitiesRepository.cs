using Microsoft.EntityFrameworkCore;
using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using SAS.EventsService.Domain.UserInterests.Entities;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Events
{
    public class NamedEntitiesRepository : BaseRepository<NamedEntity, Guid>, INamedEntitiesRepository
    {
        public NamedEntitiesRepository(AppDbContext context) : base(context)
        {

        }
    }
    public class NamedEntityTypesRepository : BaseRepository<NamedEntityType, Guid>, INamedEntityTypesRepository
    {
        public NamedEntityTypesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
