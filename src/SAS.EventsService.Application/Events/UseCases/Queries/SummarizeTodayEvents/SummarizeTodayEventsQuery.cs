using Ardalis.Result;
using SAS.SharedKernel.CQRS.Queries;

namespace SAS.EventsService.Application.Events.UseCases.Queries.GetTodaySummary
{
    public record SummarizeTodayEventsQuery() : IQuery<Result<string>>;

}
