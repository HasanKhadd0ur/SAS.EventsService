using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Topics.UseCases.Commands.DeleteTopic
{
    public record DeleteTopicCommand(Guid Id) : ICommand<Result>;

}
