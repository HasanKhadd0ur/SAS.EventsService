using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Common.Errors
{
    public static class MessageErrors
    {
        public static readonly DomainError EmptyContent =
            new("Message.EmptyContent", "Message content cannot be empty.");
    }
}
