using Ardalis.Result;
using SAS.EventsService.Application.Events.Common;
using SAS.EventsService.Domain.Events.Entities;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using System;

namespace SAS.EventsService.Application.Regions.UseCases.Commands.CreateTopic
{
    public record CreateUserInterestCommand(string InterestName, int RadiusInKm, LocationDTO Location ) : ICommand<Result<Guid>>;

}
