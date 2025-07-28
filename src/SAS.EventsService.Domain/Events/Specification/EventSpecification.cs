using SAS.EventsService.Domain.Events.Entities;
using SAS.SharedKernel.Specification;

public class BaseEventSpecification : BaseSpecification<Event>
{
    public BaseEventSpecification() : base()
    {
        AddInclude(e => e.Location );
        AddInclude(e => e.Topic);

    }
}

public class EventsByTopicNameSpecification : BaseEventSpecification
{
    public EventsByTopicNameSpecification(string topicName) : base()
    {
        Criteria = e => e.Topic.Name == topicName;
    }
}

public class EventsByRegionNameSpecification : BaseEventSpecification
{
    public EventsByRegionNameSpecification(string regionName) : base()
    {
        Criteria = e => e.Region.Name == regionName;
    }
}

public class EventsByLastUpdatedAfterSpecification : BaseEventSpecification
{
    public EventsByLastUpdatedAfterSpecification(DateTime lastUpdatedAfter) : base()
    {
        Criteria = e => e.LastUpdatedAt >= lastUpdatedAfter;
        OrderByDescending = e => e.LastUpdatedAt;

    }
}

public class EventsByCreatedAtBetweenSpecification : BaseEventSpecification
{
    public EventsByCreatedAtBetweenSpecification(DateTime from, DateTime to) : base()
    {
        Criteria = e => e.CreatedAt >= from && e.CreatedAt <= to;
    }
}
public class EventsByDateSpecification : BaseEventSpecification
{
    public EventsByDateSpecification(DateTime date) : base()
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

        Criteria = e => e.CreatedAt >= startOfDay && e.CreatedAt <= endOfDay;
    }
}

public class EventsByLocationRadiusSpecification : BaseEventSpecification
{
    public EventsByLocationRadiusSpecification(double latitude, double longitude, double radiusInKm)
    {
        //Criteria = e => GetDistanceInKm(
        //                    e.Location.Latitude,
        //                    e.Location.Longitude,
        //                    latitude,
        //                    longitude) <= radiusInKm;
    }

    private static double GetDistanceInKm(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Earth radius in KM

        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private static double ToRadians(double deg) => deg * (Math.PI / 180);
}

public class EventWithMessagesByIdSpecification : BaseSpecification<Event>
{
    public EventWithMessagesByIdSpecification(Guid eventId)
        : base(e => e.Id == eventId)
    {
        AddInclude(e => e.Messages);
    }
}
