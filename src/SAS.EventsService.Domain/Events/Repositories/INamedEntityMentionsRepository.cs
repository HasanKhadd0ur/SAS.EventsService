using SAS.EventsService.Domain.Events.Entities;
using SAS.SharedKernel.Repositories;

namespace SAS.EventsService.Domain.Events.Repositories
{
    public interface INamedEntityMentionsRepository : IRepository<NamedEntityMention, Guid>
    {
    }
}
