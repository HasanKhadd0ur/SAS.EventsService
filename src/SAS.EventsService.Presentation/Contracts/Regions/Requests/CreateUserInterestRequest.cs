using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Presentation.Contracts.Topics.Requests
{
    public record CreateUserInterestRequest(string InterestName, int RadiusInKm, LocationDTO Location);

}
