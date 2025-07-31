using SAS.EventsService.Application.Contracts.NER;
using SAS.EventsService.Application.NamedEntities.Common;
using SAS.EventsService.Domain.NamedEntities.Repositories;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace SAS.EventsService.Infrastructure.Services.NER
{
    public class HttpNERExtractor : INamedEntityExtractor
    {
        private readonly HttpClient _httpClient;
        private readonly INamedEntityTypesRepository _typeRepository;
        private readonly ILogger<HttpNERExtractor> _logger;

        // In-memory cache: normalized type name -> NamedEntityTypeDto
        private readonly Dictionary<string, NamedEntityTypeDto> _typeCache = new(StringComparer.OrdinalIgnoreCase);

        public HttpNERExtractor(HttpClient httpClient, INamedEntityTypesRepository typeRepository, ILogger<HttpNERExtractor> logger)
        {
            _httpClient = httpClient;
            _typeRepository = typeRepository;
            _logger = logger;
        }

        public async Task<List<NamedEntityDto>> Extract(string text)
        {
            try
            {
                var content = new StringContent(text, Encoding.UTF8, "text/plain");

                var response = await _httpClient.PostAsync("/ner/extract", content);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var rawEntities = JsonSerializer.Deserialize<List<RawNEREntity>>(json, options) ?? new();

                var results = new List<NamedEntityDto>();

                foreach (var entity in rawEntities)
                {
                    // Try to get type from cache
                    if (!_typeCache.TryGetValue(entity.type, out var cachedType))
                    {
                        // Query the repository and cache result
                        var typeFromRepo = await _typeRepository.GetByNormalizedNameAsync(entity.type);
                        if (typeFromRepo is not null)
                        {
                            cachedType = new NamedEntityTypeDto
                            {
                                Id = typeFromRepo.Id,
                                NormalisedName = typeFromRepo.NormalisedName,
                                TypeName = typeFromRepo.TypeName
                            };
                            _typeCache[entity.type] = cachedType;
                        }
                        else
                        {
                            _logger.LogWarning("NER type '{Type}' not found in repository, skipping entity '{Entity}'", entity.type, entity.text);
                            continue;
                        }
                    }

                    results.Add(new NamedEntityDto
                    {
                        EntityName = entity.text,
                        Type = cachedType
                    });
                }

                return results;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Failed to connect to NER service at {BaseAddress}", _httpClient.BaseAddress);
                return new List<NamedEntityDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during NER extraction");
                return new List<NamedEntityDto>();
            }
        }

        private class RawNEREntity
        {
            public string text { get; set; }
            public string type { get; set; }
        }
    }
}
