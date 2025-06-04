using Ardalis.Result;
using SAS.EventsService.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetTodaySummary
{
    public record SummarizeTodayEventsQuery() : IQuery<Result<string>>;

}