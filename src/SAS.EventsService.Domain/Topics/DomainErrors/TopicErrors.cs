using SAS.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Common.Errors
{
    public static class TopicErrors
    {
        public static readonly DomainError UnExistTopic =
            new("Topic.UNExistTopic", "Topic content un exist.");
    }
}
