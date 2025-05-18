using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Events.DomainErrors
{
    public static class MessageErrors
    {
        public static readonly DomainError EmptyContent =
            new("Message.EmptyContent", "Message content cannot be empty.");
    }
}
