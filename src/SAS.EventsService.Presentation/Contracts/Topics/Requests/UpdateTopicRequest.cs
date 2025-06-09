using System;

namespace SAS.EventsService.Presentation.Contracts.Topics.Requests
{
    public record UpdateTopicRequest(Guid Id, string Name,string Description);

}
