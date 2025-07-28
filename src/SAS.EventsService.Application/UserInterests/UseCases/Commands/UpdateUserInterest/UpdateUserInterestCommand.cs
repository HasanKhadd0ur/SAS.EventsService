using Ardalis.Result;
using SAS.EventService.Domain.Entities;
using SAS.EventsService.Application.Events.Common;
using SAS.SharedKernel.CQRS.Commands;

namespace SAS.EventsService.Application.Regions.UseCases.Commands.UpdateTopic
{
    public record UpdateUserInterestCommand(Guid Id, string InterestName, int radiusInKm , LocationDTO Location) : ICommand<Result>;

}
