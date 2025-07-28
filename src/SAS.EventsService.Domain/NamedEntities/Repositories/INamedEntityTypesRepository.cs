using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.NamedEntities.Repositories
{
    public interface INamedEntityTypesRepository : IRepository<NamedEntityType, Guid>
    {
    }
}
