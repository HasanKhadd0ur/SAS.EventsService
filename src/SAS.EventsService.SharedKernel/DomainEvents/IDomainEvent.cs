using MediatR;
using System;

namespace SAS.EventsService.SharedKernel.DomainEvents
{
    public interface IDomainEvent : INotification
    {
        public DateTime DateOccurred { get; set; }

    }
}
