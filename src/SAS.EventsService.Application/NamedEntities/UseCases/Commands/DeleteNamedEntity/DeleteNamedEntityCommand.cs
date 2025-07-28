using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.NamedEntities.UseCases.Commands.DeleteNamedEntity
{
    public record DeleteNamedEntityCommand(Guid Id) : ICommand<Result>;

}
