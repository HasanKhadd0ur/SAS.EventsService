namespace SAS.EventsService.SharedKernel.CQRS.Commands
{
    public interface ILoggableCommand<out TResponse> : ICommand<TResponse>
    {
    }
}
