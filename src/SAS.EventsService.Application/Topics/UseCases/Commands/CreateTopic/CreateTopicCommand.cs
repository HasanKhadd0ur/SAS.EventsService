using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using System;

namespace SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic
{
    public record CreateTopicCommand(string Name) : ICommand<Result<Guid>>;

}
