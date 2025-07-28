using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.MarkEventAsReviewed
{
    public record MarkEventAsReviewedCommand(Guid EventId) : ICommand<Result>;
}
