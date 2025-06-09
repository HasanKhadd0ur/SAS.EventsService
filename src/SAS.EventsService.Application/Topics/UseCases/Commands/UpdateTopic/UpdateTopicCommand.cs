using Ardalis.Result;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Topics.UseCases.Commands
{
    public record UpdateTopicCommand(Guid Id, string Name, string Description) : ICommand<Result>;

}
