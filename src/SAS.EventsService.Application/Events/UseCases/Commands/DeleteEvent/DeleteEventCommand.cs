using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.DeleteEvent
{
    public record DeleteEventCommand(Guid Id) : ICommand<Result>;
}
