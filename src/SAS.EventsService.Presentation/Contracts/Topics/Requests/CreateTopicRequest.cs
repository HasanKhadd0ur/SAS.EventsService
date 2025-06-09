using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Contracts.Topics.Requests
{
    public record CreateTopicRequest(string Name, string Description);

}
