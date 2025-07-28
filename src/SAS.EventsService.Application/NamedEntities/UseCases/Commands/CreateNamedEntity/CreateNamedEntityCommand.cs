using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Commands.CreateNamedEntity
{
    public record CreateNamedEntityCommand(string EntityName, Guid TypeId)
        : ICommand<Result<Guid>>;
}
