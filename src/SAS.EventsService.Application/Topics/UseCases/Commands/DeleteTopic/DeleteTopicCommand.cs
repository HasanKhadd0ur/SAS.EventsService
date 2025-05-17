using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Topics.UseCases.Commands.DeleteTopic
{
    public record DeleteTopicCommand(Guid Id) : ICommand<Result>;

}
