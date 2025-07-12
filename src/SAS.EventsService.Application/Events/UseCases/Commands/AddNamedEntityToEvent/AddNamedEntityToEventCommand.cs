using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddNamedEntityToEvent
{
    public record AddNamedEntityToEventCommand(Guid EventId, Guid NamedEntityId) : ICommand<Result>;
}
