using SAS.EventService.Domain.Entities;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.Topics.Repositories
{
    public interface ITopicsRepository : IRepository<Topic, Guid>
    {
        Task<Topic> GetByNameAsync(string topicName);
    }
}
