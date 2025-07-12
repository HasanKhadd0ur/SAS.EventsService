using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Commands.CreateNamedEntity
{
    public record CreateNamedEntityCommand(string EntityName, Guid TypeId)
        : ICommand<Result<Guid>>;
}
