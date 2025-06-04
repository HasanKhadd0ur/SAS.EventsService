namespace SAS.EventsService.Application.Contracts.LLMs
{
    public interface ILLMClient
    {

        Task<string> GenerateContentAsync(string prompt, CancellationToken cancellationToken = default);

    }
}