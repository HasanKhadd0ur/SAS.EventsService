using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Application.Contracts.Notfications
{
    public interface INotificationService
    {
        Task NotifyUserAsync(Guid userId, Guid eventId);
    }
}
