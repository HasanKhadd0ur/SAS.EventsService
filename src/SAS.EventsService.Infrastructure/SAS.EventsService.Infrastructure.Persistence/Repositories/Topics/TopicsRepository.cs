using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Topics.Repositories;
using SAS.EventsService.Infrastructure.Persistence.AppDataContext;
using SAS.EventsService.Infrastructure.Persistence.Repositories.Base;
using SAS.SharedKernel.Specification;

namespace SAS.EventsService.Infrastructure.Persistence.Repositories.Topics
{
    public class TopicsRepository : BaseRepository<Topic, Guid>, ITopicsRepository
    {
        public TopicsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Topic> GetByNameAsync(string name)
        {
            var spec = new BaseSpecification<Topic>(t => t.Name == name);
            return await FirstOrDefaultAsync(spec);
        }
    }
}
