using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Regions.UseCases.Commands.DeleteTopic
{
    public record DeleteUserInterestCommand(Guid Id) : ICommand<Result>;

}
