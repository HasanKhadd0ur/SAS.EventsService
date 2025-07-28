using Ardalis.Result;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Regions.UseCases.Commands.DeleteTopic
{
    public record DeleteUserInterestCommand(Guid Id) : ICommand<Result>;

}
