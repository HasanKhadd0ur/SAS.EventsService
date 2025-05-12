using MediatR;

namespace SAS.EventsService.SharedKernel.CQRS.Commands
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
