using MediatR;

namespace SAS.EventsService.SharedKernel.CQRS.Commands
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
                         where TCommand : ICommand<TResponse>
    {
    }
}
