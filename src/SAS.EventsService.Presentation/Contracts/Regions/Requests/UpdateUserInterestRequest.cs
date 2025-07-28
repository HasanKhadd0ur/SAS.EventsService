using SAS.EventsService.Application.Events.Common;
using System;

namespace SAS.EventsService.Presentation.Contracts.Topics.Requests
{
    public record UpdateUserInterestRequest(Guid Id, string InterestName, int radiusInKm, LocationDTO Location);

}
