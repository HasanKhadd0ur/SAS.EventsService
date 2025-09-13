using MediatR;
using SAS.SharedKernel.CQRS.Commands;
using Serilog;

namespace SAS.EventsService.Application.Behaviors.LoggingBehavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ILoggableCommand<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            Log.Information("Starting request: {RequestName} at {DateTime}", requestName, DateTime.UtcNow);

            try
            {
                var response = await next();
                Log.Information("Completed request: {RequestName} at {DateTime}", requestName, DateTime.UtcNow);
                return response;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Request {RequestName} failed at {DateTime}", requestName, DateTime.UtcNow);
                throw;
            }
        }
    }
}
