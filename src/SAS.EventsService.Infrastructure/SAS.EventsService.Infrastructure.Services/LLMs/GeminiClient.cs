using Microsoft.Extensions.Configuration;
using SAS.EventsService.Application.Contracts.LLMs;
using System.Text;
using System.Text.Json;

namespace SAS.EventsService.Infrastructure.Services.LLMs
{
    public class GeminiClient : ILLMClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini:ApiKey");
        }

        public async Task<string> GenerateContentAsync(string prompt, CancellationToken cancellationToken = default)
        {
            var url = $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[] { new { text = prompt } }
                }
            }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return $"? Gemini API error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";

            var responseContent = await response.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(responseContent);
                return doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString() ?? "Cannount Generate Report";
            }
            catch
            {
                return "Unable to generate report";
            }
        }
    }

}
