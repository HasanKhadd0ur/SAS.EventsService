using SAS.EventsService.Application.Contracts.NER;
using SAS.EventsService.Application.Events.Common;
using System.Net.Http.Json;

namespace SAS.EventsService.Infrastructure.Services.LLMs
{
    public class HttpNERExtractor : INamedEntityExtractor
    {
        private readonly HttpClient _httpClient;

        public HttpNERExtractor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NamedEntityDto>> Extract(string text)
        {
            var response = await _httpClient.PostAsJsonAsync("/extract", new { text });
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<NamedEntityDto>>();
            return result ?? new();
        }
    }


}
