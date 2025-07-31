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
        public async Task<NamedEntity?> GetByNameAndTypeAsync(string name, Guid typeId)
        {
            return await dbSet.FirstOrDefaultAsync(e =>
                    e.EntityName.ToLower() == name.ToLower() &&
                    e.TypeId == typeId);
        }
    }
    public class NamedEntityTypesRepository : BaseRepository<NamedEntityType, Guid>, INamedEntityTypesRepository
    {
        public NamedEntityTypesRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<NamedEntityType?> GetByNormalizedNameAsync(string normalizedName)
        {
            return await dbSet
                .FirstOrDefaultAsync(t =>
                    t.NormalisedName.ToLower() == normalizedName.ToLower());
        }
    }
}
