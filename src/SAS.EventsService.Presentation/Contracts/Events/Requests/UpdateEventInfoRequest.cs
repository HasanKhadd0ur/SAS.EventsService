using SAS.EventsService.Domain.Events.ValueObjects;
using System;

namespace SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent
{
    public record UpdateEventInfoRequest(Guid EventId, EventInfo NewEventInfo);

}
