using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SAS.EventsService.Application.Contracts.LLMs;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.EventsService.SharedKernel.CQRS.Queries;
using System.Text.Json;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetTodaySummary
{
    public class SummarizeTodayEventsQueryHandler : IQueryHandler<SummarizeTodayEventsQuery, Result<string>>
    {
        private readonly IEventsRepository _eventRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILLMClient _llmClient;
        
        public SummarizeTodayEventsQueryHandler(
            IEventsRepository eventRepository,
            IDateTimeProvider dateTimeProvider,
            ILLMClient llmClient)
        {
            _eventRepository = eventRepository;
            _dateTimeProvider = dateTimeProvider;
            _llmClient = llmClient;
        }

        public async Task<Result<string>> Handle(SummarizeTodayEventsQuery request, CancellationToken cancellationToken)
        {
            var today = _dateTimeProvider.UtcNow.Date;
            var from = today;
            var to = today.AddDays(1).AddTicks(-1);

            var spec = new EventsByCreatedAtBetweenSpecification(from, to);
            var events = await _eventRepository.ListAsync(spec);

            if (events is null || events.Count() == 0)
                return Result.Invalid(EventErrors.NoEvents);

            var prompt = BuildGeminiPrompt(events);
            var summary = await _llmClient.GenerateContentAsync(prompt, cancellationToken);

            return Result.Success(summary);
        }

        private string BuildGeminiPrompt(IEnumerable<Event> events)
        {
            var json = JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true });
            return $"قم بتقديم تقرير عن الأحداث التالية ({events.Count()}) لتكون مناسبة للنشر على موقع إخباري، بصيغة Markdown (استخدم العناوين والعناصر النقطية إن لزم، ولا تكرر):\n\n{json}";
        }
    }

}