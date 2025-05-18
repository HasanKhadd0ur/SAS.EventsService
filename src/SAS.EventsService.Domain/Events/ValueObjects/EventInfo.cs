namespace SAS.EventsService.Domain.Events.ValueObjects
{
    public record EventInfo(
        string Title,
        string Summary,
        int SentimentScore,
        string SentimentLabel
        );
}
