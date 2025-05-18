using MediatR;

namespace SAS.EventsService.SharedKernel.DomainEvents
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
