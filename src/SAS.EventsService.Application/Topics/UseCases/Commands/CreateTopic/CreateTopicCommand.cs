using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;
using System;

namespace SAS.EventsService.Application.Topics.UseCases.Commands.CreateTopic
{
    public record CreateTopicCommand(string Name,string IconUrl, string Description) : ICommand<Result<Guid>>;

}
