using Ardalis.Result;
using SAS.EventsService.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Common.Errors;

public static class EventErrors
{
    public static readonly DomainError TitleTooLong =
        new("Event.TitleTooLong", "The event title exceeds the maximum allowed length.");

    public static readonly DomainError SummaryTooLong =
        new("Event.SummaryTooLong", "The event summary exceeds the maximum allowed length.");
 
    public static readonly DomainError UnExistEvent =
        new("Event.UnExistEvent", "Event un exist.");

    public static readonly DomainError NoEvents =
        new("Event.NoEvents", "There are no events today exist.");

}
