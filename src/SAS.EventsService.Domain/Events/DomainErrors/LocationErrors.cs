using SAS.SharedKernel.DomainErrors;

namespace SAS.EventsService.Domain.Common.Errors;

public static class LocationErrors
{
    public static readonly DomainError InvalidCoordinates =
        new("Location.InvalidCoordinates", "Latitude or longitude values are invalid.");
}
