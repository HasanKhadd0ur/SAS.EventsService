using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SAS.EventsService.Application.Events.UseCases.Commands.CreateEvent;
using SAS.EventsService.Presentation.Controllers.ApiBase;
using System.Threading.Tasks;

namespace SAS.EventsService.Presentation.Controllers
{
    public class EventsController : APIController
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
        {
            var command = new CreateEventFromDetectionCommand(
                request.EventInfo,
                request.TopicName,
                request.CountryName,
                request.RegionName,
                request.CityName,
                request.Latitude,
                request.Longitude
            );

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
