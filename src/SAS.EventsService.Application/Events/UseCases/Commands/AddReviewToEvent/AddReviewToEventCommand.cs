using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.AddReviewToEvent
{
    public record AddReviewToEventCommand(
        Guid EventId,
        Guid UserId,
        string UserName,
        string Comment
    ) : ICommand<Result>;

}
