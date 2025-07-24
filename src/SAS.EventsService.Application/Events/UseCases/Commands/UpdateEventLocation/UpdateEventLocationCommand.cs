using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventLocation
{
    public record UpdateEventLocationCommand(Guid EventId, LocationDTO Location) : ICommand<Result>;
}
