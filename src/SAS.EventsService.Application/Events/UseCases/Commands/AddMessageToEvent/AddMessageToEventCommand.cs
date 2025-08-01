using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddMessageToEvent
{
    public record AddMessageToEventCommand(Guid EventId, MessageDto NewMessage) : ICommand<Result>;

}
