using SAS.EventsService.SharedKernel.Entities;
using SAS.EventsService.SharedKernel.Utilities;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace SAS.EventsService.SharedKernel.Repositories
{
    public interface IReadRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<List<T>> ListAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
    }
}
