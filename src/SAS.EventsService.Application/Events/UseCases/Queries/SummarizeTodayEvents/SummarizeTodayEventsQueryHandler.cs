using Ardalis.Result;
using MediatR;
using SAS.EventsService.Application.Contracts.LLMs;
using SAS.EventsService.Application.Contracts.Providers;
using SAS.EventsService.Domain.Common.Errors;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.Domain.Events.Repositories;
using SAS.SharedKernel.CQRS.Queries;
using System.Text;
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

            if (events is null || !events.Any())
                return Result.Invalid(EventErrors.NoEvents);

            var prompt = BuildPrompt(events);
            var summary = await _llmClient.GenerateContentAsync(prompt, cancellationToken);

            return Result.Success(summary);
        }

        private string BuildPrompt(IEnumerable<Event> events)
        {
            var sb = new StringBuilder();

            sb.AppendLine("ﬁ„ » Õ·Ì· Ê ·ŒÌ’ «·√Õœ«À «· «·Ì…° Ê«ﬂ »  ﬁ—Ì—« ≈Œ»«—Ì« »’Ì€… Markdown „‰«”» ··‰‘—. «” Œœ„ ⁄‰«ÊÌ‰ —∆Ì”Ì… Ê‰ﬁ«ÿ ›—⁄Ì… ≈‰ ·“„:");
            sb.AppendLine();
            sb.AppendLine("### ﬁ«∆„… «·√Õœ«À:");

            int index = 1;
            foreach (var ev in events)
            {
                sb.AppendLine($"#### «·ÕœÀ {index++}:");
                sb.AppendLine($"- **«·⁄‰Ê«‰:** {ev.EventInfo.Title}");
                sb.AppendLine($"- **«·Ê’›:** {ev.EventInfo.Summary}");
                sb.AppendLine($"- **«·„Êﬁ⁄:** {ev.Location?.ToString() ?? "€Ì— „Õœœ"}");
                sb.AppendLine($"- **«·Êﬁ :** {ev.CreatedAt.ToString("yyyy-MM-dd HH:mm")}");
                sb.AppendLine();
            }

            sb.AppendLine("Ì—ÃÏ ﬂ «»… „·Œ’ «Õ —«›Ì »‰«¡ ⁄·Ï Â–Â «·„⁄·Ê„« .");

            return sb.ToString();
        }
    }
}
