using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.Events.Repositories
{
    public interface IMessagesRepository : IRepository<Message, Guid>
    {
    }
}
