using MediatR;

namespace SAS.EventsService.SharedKernel.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    {
    }
}
