using MediatR;

namespace SAS.EventsService.SharedKernel.CQRS.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

}
