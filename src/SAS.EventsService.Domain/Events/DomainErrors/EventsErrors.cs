using Ardalis.Result;
using SAS.SharedKernel.DomainErrors;

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
    
    public static readonly DomainError EmptyComment =
        new("Event.EmptyComment", "Review comment cannot be empty.");
   
    public static readonly DomainError UnExistReview =
        new("Event.UnExistReview", "Un Exist Review.");
    
    public static readonly DomainError Forbiden =
        new("Event.Forbiden", "Forbiden Review.");

}
