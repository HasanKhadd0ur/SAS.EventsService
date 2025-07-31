using SAS.EventsService.Domain.NamedEntities.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.NamedEntities.Repositories
{
    public interface INamedEntitiesRepository : IRepository<NamedEntity, Guid>
    {
        Task<NamedEntity?> GetByNameAndTypeAsync(string name, Guid typeId);

    }
}
