using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Events.UseCases.Commands.ChangeEventTopic
{
    public record ChangeEventTopicCommand(Guid EventId, Guid TopicId) : ICommand<Result>;
}