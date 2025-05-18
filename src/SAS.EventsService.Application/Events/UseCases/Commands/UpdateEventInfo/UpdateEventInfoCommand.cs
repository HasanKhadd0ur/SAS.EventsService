using Ardalis.Result;
using SAS.EventsService.Domain.Events.ValueObjects;
using SAS.EventsService.SharedKernel.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.EventsService.Application.Events.UseCases.Commands.UpdateEventInfo
{
    public record UpdateEventInfoCommand(Guid EventId, EventInfo NewEventInfo) : ICommand<Result>;
}
