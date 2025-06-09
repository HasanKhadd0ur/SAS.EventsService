namespace SAS.EventsService.Domain.Events.ValueObjects
{
    public record EventInfo(
        string Title,
        string Summary,
        double SentimentScore,
        string SentimentLabel
        );
}
