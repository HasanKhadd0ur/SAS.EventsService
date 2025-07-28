using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.BulkAddMessagesToEvent
{
    public record BulkAddMessagesToEventCommand(Guid EventId, List<MessageDto> NewMessages) : ICommand<Result>;
}
